using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using log4net;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly IUserForgotPasswordTokenRepository _userForgotPasswordTokenRepository;
        private readonly EmailHelper _emailHelper;
        private readonly ILog _log = LogManager.GetLogger(LogConstant.LogName.AuthentiationFeature);

        public AuthService(IUserRepository userRepository
            , IConfiguration configuration
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor
            , IUserRefreshTokenRepository userRefreshTokenRepository
            , IMailTemplateRepository mailTemplateRepository
            , IUserForgotPasswordTokenRepository userForgotPasswordTokenRepository
            , EmailHelper emailHelper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _mailTemplateRepository = mailTemplateRepository;
            _userForgotPasswordTokenRepository = userForgotPasswordTokenRepository;
            _emailHelper = emailHelper;
        }


        // ================== LOGIN ==================
        // For login
        public async Task<AuthResponseDto> AuthenticateAsync(LoginRequestDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.UsernameOrEmail))
            {
                throw new ArgumentException(AuthenticationErrorMessage.UsernameOrEmailCanNotBeNull);
            }

            if (string.IsNullOrWhiteSpace(loginDto.Password))
            {
                throw new ArgumentException(AuthenticationErrorMessage.PasswordCanNotBeNull);
            }

            var user = await _userRepository.GetUserByUsernameOrEmailAsync(loginDto.UsernameOrEmail);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                _log.Info(LogMessage.Infor.LoginFailed_InvalidCredentials(loginDto.UsernameOrEmail));
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.InvalidCredentials);
            }
            else if (user != null && !user.IsEnable)
            {
                _log.Info(LogMessage.Infor.LoginFailed_AccountDisabled(loginDto.UsernameOrEmail));
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.AccountDisabled);
            }

            // Generate tokens
            var authTokensResponse = TokenHelper.GenerateJwtTokens(user, _configuration);

            // Set access token
            SetAccessToken(authTokensResponse.AccessToken);
            _log.Info(LogMessage.Infor.SetAccessTokenToCookie(loginDto.UsernameOrEmail));

            // Revoke old refresh token
            await DeleteOldRefreshToken();
            _log.Info(LogMessage.Infor.DeleteOldRefreshToken(loginDto.UsernameOrEmail));

            // Set refresh token
            await SetRefreshTokenAsync(user, authTokensResponse.RefreshToken);
            _log.Info(LogMessage.Infor.SetRefreshToken(loginDto.UsernameOrEmail));

            _log.Info(LogMessage.Infor.LoginSuccess(loginDto.UsernameOrEmail));
            // Return response
            return new AuthResponseDto { Message = AuthenticationSuccessMessage.LoginSuccess,
                UserDataAuthResponse = new UserDataAuthResponse
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Username = user.Username,
                    RoleCode = user.RoleCode
                }
            };
        }

        // Set aceess token to cookie
        private void SetAccessToken(string accessToken)
        {
            var expiresTime = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:AccessTokenExpiryMinutes"]));
            // Set to HTTP-Only Cookies
            TokenHelper.SetTokenToCookie(_httpContextAccessor, JwtConstant.AccessTokenCookieName, accessToken, expiresTime);
        }

        // Set refresh token to cookie and database
        private async Task SetRefreshTokenAsync(User user, string refreshToken)
        {
            DateTime expiresTime = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtSettings:RefreshTokenExpiryDays"]));

            // Set to HTTP-Only Cookies
            TokenHelper.SetTokenToCookie(_httpContextAccessor, JwtConstant.RefreshTokenCookieName, refreshToken, expiresTime);

            // Save to databases
            var userRefreshToken = new UserRefreshToken()
            {
                UserId = user.UserId,
                RefreshToken = refreshToken,
                ExpiresAt = expiresTime
            };

            await _userRefreshTokenRepository.AddAsync(userRefreshToken);
        }

        // Delete old token from database
        private async Task DeleteOldRefreshToken()
        {
            var oldRefreshTokenString = TokenHelper.GetTokenFromCookie(_httpContextAccessor, JwtConstant.RefreshTokenCookieName);
            if (oldRefreshTokenString != null)
            {
                TokenHelper.RemoveTokenFromCookie(_httpContextAccessor, JwtConstant.RefreshTokenCookieName);

                var oldRefreshToken = await _userRefreshTokenRepository.GetByTokenAsync(oldRefreshTokenString);
                if (oldRefreshToken != null)
                {
                    //oldRefreshToken.IsRevoked = true;
                    //oldRefreshToken.RevokedAt = DateTime.Now;

                    await _userRefreshTokenRepository.DeleteAsync(oldRefreshToken.UserRefreshTokenId);
                }
            }
        }
        // ===================================================


        // ================== LOGOUT =========================
        public async Task<AuthResponseDto> LogoutAsync()
        {
            // Get RT from cookie
            var refreshTokenStringFromCookie = TokenHelper.GetTokenFromCookie(_httpContextAccessor, JwtConstant.RefreshTokenCookieName);
            if (refreshTokenStringFromCookie != null)
            {
                var userRefreshToken = await _userRefreshTokenRepository.GetByTokenAsync(refreshTokenStringFromCookie);
                if (userRefreshToken != null)
                {
                    await _userRefreshTokenRepository.DeleteAsync(userRefreshToken.UserRefreshTokenId);
                }

                TokenHelper.RemoveTokenFromCookie(_httpContextAccessor, JwtConstant.AccessTokenCookieName);
                TokenHelper.RemoveTokenFromCookie(_httpContextAccessor, JwtConstant.RefreshTokenCookieName);
            }
            else
            {
                return new AuthResponseDto { Message = AuthenticationErrorMessage.NotFoundRefreshToken };
            }

            return new AuthResponseDto { Message = AuthenticationSuccessMessage.LogoutSuccess };
        }
        // ===================================================


        // ================== REFRESH TOKEN ==================
        // For refresh token process request from controller - OLD METHOD
        public async Task<AuthResponseDto> RefreshTokensAsync()
        {
            // Get RT from cookie
            var refreshTokenInCookie = TokenHelper.GetTokenFromCookie(_httpContextAccessor, JwtConstant.RefreshTokenCookieName);

            // Get saved RT from database
            var userRefreshToken = await ValidateRefreshTokenAsync(refreshTokenInCookie!);

            // Get RT owner information from DB
            var refreshTokenOwner = await ValidateUserAsync(userRefreshToken.UserId);

            // Generate tokens
            var authTokensResponse = TokenHelper.GenerateJwtTokens(refreshTokenOwner, _configuration);

            // Set access token
            SetAccessToken(authTokensResponse.AccessToken);

            // Revoke old refresh token
            await DeleteOldRefreshToken();

            // Set refresh token
            await SetRefreshTokenAsync(refreshTokenOwner, authTokensResponse.RefreshToken);

            return new AuthResponseDto
            {
                Message = AuthenticationSuccessMessage.LoginSuccess,
                UserDataAuthResponse = new UserDataAuthResponse
                {
                    UserId = refreshTokenOwner.UserId,
                    Email = refreshTokenOwner.Email,
                    Username = refreshTokenOwner.Username,
                    RoleCode = refreshTokenOwner.RoleCode
                }
            };
        }

        // For refesh token process auto request by system - CURRENT METHOD
        public async Task<AuthTokensResponseDto> AutoRefreshTokensAsync()
        {
            try
            {
                // Get RT from cookie
                var refreshTokenInCookie = TokenHelper.GetTokenFromCookie(_httpContextAccessor, JwtConstant.RefreshTokenCookieName);
                if (string.IsNullOrEmpty(refreshTokenInCookie))
                {
                    Console.WriteLine("Not found refresh token on cookie");
                }

                // Get saved RT from database
                var userRefreshToken = await ValidateRefreshTokenAsync(refreshTokenInCookie!);

                // Get RT owner information from DB
                var refreshTokenOwner = await ValidateUserAsync(userRefreshToken.UserId);

                // Revoke old refresh token
                await DeleteOldRefreshToken();

                // Generate tokens
                var authTokensResponse = TokenHelper.GenerateJwtTokens(refreshTokenOwner, _configuration);

                // Set access token
                SetAccessToken(authTokensResponse.AccessToken);

                // Set refresh token
                await SetRefreshTokenAsync(refreshTokenOwner, authTokensResponse.RefreshToken);

                return authTokensResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // To check is this refresh token valid
        private async Task<UserRefreshToken> ValidateRefreshTokenAsync(string refreshToken)
        {
            var userRefreshToken = await _userRefreshTokenRepository.GetByTokenAsync(refreshToken);
            if (userRefreshToken == null)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.InvalidRefreshToken);
            } else if (userRefreshToken.ExpiresAt <= DateTime.Now)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.ExpiredRefreshToken);
            }
            return userRefreshToken;
        }

        // To check is this user having this token valid
        private async Task<User> ValidateUserAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.InvalidRefreshToken);
            }
            else if (!user.IsEnable)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.AccountDisabled);
            }
            return user;
        }

        // To check is this user valid, return bool
        public async Task<bool> IsUserValidAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user != null && user.IsEnable;
        }
        // =====================================================


        // ================== CHANGE PASSWORD ==================
        public async Task<AuthResponseDto> ChangePasswordAsync(string userId, ChangePasswordRequestDto changePasswordRequest)
        {
            // To check is new password match regex
            if (!RegexHelper.IsMatchRegex(changePasswordRequest.NewPassword, RegexConstant.PasswordRegex))
            {
                throw new ArgumentException(AuthenticationErrorMessage.WrongPasswordFormat);
            }

            // To check is confirm new password correct
            if (changePasswordRequest.NewPassword != changePasswordRequest.ConfirmNewPassword)
            {
                throw new ArgumentException(AuthenticationErrorMessage.WrongConfirmPassword);
            }

            // Get user information
            var user = await _userRepository.GetUserByIdAsync(userId);

            // To check is this user not exists in the database
            if (user == null)
            {
                throw new KeyNotFoundException(AuthenticationErrorMessage.UserNotFound);
            }

            // To check is entered old password correct
            if (!BCrypt.Net.BCrypt.Verify(changePasswordRequest.OldPassword, user.Password))
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.WrongOldPassword);
            }

            // To check is new password and old password is the same
            if (BCrypt.Net.BCrypt.Verify(changePasswordRequest.NewPassword, user.Password))
            {
                throw new ArgumentException(AuthenticationErrorMessage.NewPasswordSameAsOldPassword);
            }

            // Update new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordRequest.NewPassword);
            await _userRepository.UpdateUserAsync(user);

            await SendChangedPasswordMailAsync(user);

            return new AuthResponseDto
            {
                Message = AuthenticationSuccessMessage.ChangePasswordSuccess,
                UserDataAuthResponse = new UserDataAuthResponse()
            };
        }

        // To send announcement to user 
        private async Task SendChangedPasswordMailAsync(User user)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.ChangedPasswordEmailId);
            if (mailTemplate != null)
            {
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{USER_FULLNAME}}", user.Fullname)
                    .Replace("{{CHANGE_DATE}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                var t = new Thread(() => _emailHelper.SendEmailMultiThread(user.Email, mailTemplate.TemplateTitle, mailTemplate.Content));
                t.Start();
            }
        }
        // =====================================================


        // ================== FORGOT PASSWORD ==================
        public async Task<AuthResponseDto> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException(AuthenticationErrorMessage.UserNotFound);
            }

            if (!user.IsEnable)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.AccountDisabled);
            }

            var forgotPasswordSettingsSection = _configuration.GetSection("ForgotPasswordSettings");
            var forgotPasswordToken = TokenHelper.GenerateForgotPasswordToken(_configuration);

            var userForgotPasswordToken = new UserForgotPasswordToken()
            {
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(Double.Parse(forgotPasswordSettingsSection["AccessTokenExpiryMinutes"] ?? "30")),
                ForgotPasswordToken = forgotPasswordToken,
                IsRevoked = false,
                UserId = user.UserId
            };

            await _userForgotPasswordTokenRepository.AddAsync(userForgotPasswordToken);

            await SendForgotPasswordTokenMailAsync(forgotPasswordSettingsSection, user, userForgotPasswordToken);

            return new AuthResponseDto
            {
                Message = AuthenticationSuccessMessage.SendForgotPasswordTokenSuccess,
                UserDataAuthResponse = new UserDataAuthResponse()
            };
        }

        // To send token to user 
        private async Task SendForgotPasswordTokenMailAsync(IConfigurationSection configurationSection, User user, UserForgotPasswordToken userForgotPasswordToken)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.ForgotPasswordTokenEmailId);
            if (mailTemplate != null)
            {
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{USER_FULLNAME}}", user.Fullname)
                    .Replace("{{CHANGE_PASSWORD_LINK}}", $"{configurationSection["ClientSetNewPasswordEndpoint"]}{userForgotPasswordToken.ForgotPasswordToken}")
                    .Replace("{{TOKEN_LIVE_TIME}}", configurationSection["AccessTokenExpiryMinutes"] ?? "30");

                var t = new Thread(() => _emailHelper.SendEmailMultiThread(user.Email, mailTemplate.TemplateTitle, mailTemplate.Content));
                t.Start();
            }
        }

        // To validate token in user request is valid and return to controller
        public async Task<AuthResponseDto> IsThisForgotPasswordTokenValid(string forgotPasswordToken, bool isReturnUserInf)
        {
            if (string.IsNullOrWhiteSpace(forgotPasswordToken))
            {
                throw new ArgumentException(AuthenticationErrorMessage.ForgotPasswordTokenCanNotBeNull);
            }

            var userForgotPasswordToken = await _userForgotPasswordTokenRepository.GetByTokenAsync(forgotPasswordToken);
            if (userForgotPasswordToken == null)
            {
                throw new KeyNotFoundException(AuthenticationErrorMessage.NotFoundForgotPasswordToken);
            }

            if (userForgotPasswordToken != null && userForgotPasswordToken.ExpiresAt <= DateTime.Now)
            {
                if (!userForgotPasswordToken.IsRevoked)
                {
                    await _userForgotPasswordTokenRepository.RevokeTokenAsync(forgotPasswordToken);
                }

                throw new UnauthorizedAccessException(AuthenticationErrorMessage.ExpiredForgotPasswordToken);
            }

            if (userForgotPasswordToken != null && userForgotPasswordToken.IsRevoked)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.RevokedForgotPasswordToken);
            }

            var tokenOwner = await _userRepository.GetUserByIdAsync(userForgotPasswordToken!.UserId);
            if (tokenOwner == null || !tokenOwner.IsEnable)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.AccountDisabled);
            }
            
            // Return user information if needed
            if (isReturnUserInf)
            {
                return new AuthResponseDto
                {
                    Message = AuthenticationSuccessMessage.ValidateForgotPasswordTokenSuccess,
                    UserDataAuthResponse = new UserDataAuthResponse()
                    {
                        UserId = tokenOwner.UserId,
                        Email = tokenOwner.Email,
                        RoleCode = tokenOwner.RoleCode,
                        Username = tokenOwner.Username
                    }
                };
            }

            return new AuthResponseDto
            {
                Message = AuthenticationSuccessMessage.ValidateForgotPasswordTokenSuccess,
                UserDataAuthResponse = new UserDataAuthResponse()
            };
        }

        public async Task<AuthResponseDto> SetNewPassword(SetNewPasswordRequestDto setNewPasswordRequest)
        {
            // To check is new password match regex
            if (!RegexHelper.IsMatchRegex(setNewPasswordRequest.NewPassword, RegexConstant.PasswordRegex))
            {
                throw new ArgumentException(AuthenticationErrorMessage.WrongPasswordFormat);
            }

            // To check is confirm new password correct
            if (setNewPasswordRequest.NewPassword != setNewPasswordRequest.ConfirmNewPassword)
            {
                throw new ArgumentException(AuthenticationErrorMessage.WrongConfirmPassword);
            }

            var authResponse = await IsThisForgotPasswordTokenValid(setNewPasswordRequest.ForgotPasswordToken, true);

            // Get user information
            var user = await _userRepository.GetUserByIdAsync(authResponse.UserDataAuthResponse.UserId);

            // Update new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(setNewPasswordRequest.NewPassword);
            await _userRepository.UpdateUserAsync(user);

            // Revoke token
            await _userForgotPasswordTokenRepository.RevokeTokenAsync(setNewPasswordRequest.ForgotPasswordToken);

            await SendChangedPasswordMailAsync(user);

            return new AuthResponseDto
            {
                Message = AuthenticationSuccessMessage.ChangePasswordSuccess,
                UserDataAuthResponse = new UserDataAuthResponse()
            };
        }

        // =====================================================

        // ============== CURRENT LOGGEDIN USER ================
        public async Task<object> GetCurrentLoggedInUserInformationAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.AuthenticatedRequired);
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
             ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception(AuthenticationErrorMessage.UserNotFound);
            }

            var userInf = await _userRepository.GetUserWithRoleByIdAsync(userId);
            if (userInf == null)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.AuthenticatedRequired);
            }

            object userDto = new UserAccountDto<object>();

            if (userInf != null && userInf.RoleCode.Equals(RoleCodeConstant.PlayerCode))
            {
                if (userInf.Player == null)
                {
                    userInf.Player = new Player();
                }
                userDto = _mapper.Map<UserAccountDto<PlayerAccountDto>>(userInf);
            }

            if (userInf != null && userInf.RoleCode.Equals(RoleCodeConstant.ManagerCode))
            {
                if (userInf.Manager == null)
                {
                    userInf.Manager = new Manager();
                }
                userDto = _mapper.Map<UserAccountDto<ManagerAccountDto>>(userInf);
            }

            if (userInf != null && userInf.RoleCode.Equals(RoleCodeConstant.PresidentCode))
            {
                if (userInf.President == null)
                {
                    userInf.President = new President();
                }
                userDto = _mapper.Map<UserAccountDto<PresidentAccountDto>>(userInf);
            }

            if (userInf != null && userInf.RoleCode.Equals(RoleCodeConstant.CoachCode))
            {
                if (userInf.Coach == null)
                {
                    userInf.Coach = new Coach();
                }
                userDto = _mapper.Map<UserAccountDto<CoachAccountDto>>(userInf);
            }

            if (userInf != null && userInf.RoleCode.Equals(RoleCodeConstant.ParentCode))
            {
                if (userInf.Parent == null)
                {
                    userInf.Parent = new Parent();
                }
                userDto = _mapper.Map<UserAccountDto<ParentAccountDto>>(userInf);
            }

            return userDto;
        }
        // =====================================================
    }
}
