using System.Text.Json;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BasketballAcademyManagementSystemAPI.Common.Converter;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AccountMessage;
using System.Text;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;
using static BasketballAcademyManagementSystemAPI.Common.Messages.MemberRegistrationSessionMessage;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailHelper _emailHelper;


        public AccountService(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IPlayerRepository playerRepository,
            IMailTemplateRepository mailTemplateRepository,
            EmailHelper emailHelper)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _playerRepository = playerRepository;
            _mailTemplateRepository = mailTemplateRepository;
            _emailHelper = emailHelper;
        }

        public async Task<AccountResponseDto> UpdateProfileAsync(object userUpdateJsonData)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters =
                        {
                            new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" },
                            new DateOnlyConverter()
                        }
                };

                var userNewData = JsonConvert.DeserializeObject<User>(userUpdateJsonData.ToString()!, settings);

                var updatingUser = await _userRepository.GetUserWithRoleByIdAsync(userNewData!.UserId);
                if (updatingUser != null)
                {
                    updatingUser.Fullname = userNewData.Fullname;
                    updatingUser.Phone = userNewData.Phone;
                    updatingUser.Address = userNewData.Address;
                    updatingUser.DateOfBirth = userNewData.DateOfBirth;
                    updatingUser.UpdatedAt = DateTime.Now;

                    var jObject = JsonConvert.DeserializeObject<dynamic>(userUpdateJsonData.ToString()!);
                    string roleSpecificationJson = jObject!.roleInformation.ToString();
                    if (updatingUser.RoleCode.Equals(RoleCodeConstant.ManagerCode))
                    {
                        var updatingManager = JsonConvert.DeserializeObject<Manager>(roleSpecificationJson);
                        if (updatingManager != null)
                        {
                            updatingUser.Manager = new Manager();
                            updatingUser.Manager.UserId = updatingManager.UserId;
                            updatingUser.Manager.TeamId = updatingManager.TeamId;
                            updatingUser.Manager.BankName = updatingManager.BankName;
                            updatingUser.Manager.BankAccountNumber = updatingManager.BankAccountNumber;
                            updatingUser.Manager.PaymentMethod = updatingManager.PaymentMethod;
                            updatingUser.Manager.BankBinId = updatingManager.BankBinId;
                        }
                    }

                    if (updatingUser.RoleCode.Equals(RoleCodeConstant.CoachCode))
                    {
                        var updatingCoach = JsonConvert.DeserializeObject<Coach>(roleSpecificationJson);
                        if (updatingCoach != null)
                        {
                            updatingUser.Coach = new Coach();
                            updatingUser.Coach.UserId = updatingCoach.UserId;
                            updatingUser.Coach.Bio = updatingCoach.Bio;
                            updatingUser.Coach.TeamId = updatingCoach.TeamId;
                            updatingUser.Coach.CreatedByPresidentId = updatingCoach.CreatedByPresidentId;
                            updatingUser.Coach.ContractStartDate = updatingUser.Coach.ContractStartDate;
                            updatingUser.Coach.ContractEndDate = updatingUser.Coach.ContractEndDate;
                        }
                    }

                    if (updatingUser.Parent != null && updatingUser.RoleCode.Equals(RoleCodeConstant.ParentCode))
                    {
                        var updatingParent = JsonConvert.DeserializeObject<Parent>(roleSpecificationJson);
                        if (updatingParent != null)
                        {
                            updatingUser.Parent = new Parent();
                            updatingUser.Parent.UserId = updatingParent.UserId;
                            updatingUser.Parent.CitizenId = updatingParent.CitizenId;
                            updatingUser.Parent.CreatedByManagerId = updatingParent.CreatedByManagerId;
                        }
                    }

                    await _userRepository.UpdateUserAsync(updatingUser);
                }

                return new AccountResponseDto() { Message = AccountSuccessMessage.UpdateAccountSuccess };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating profile: " + ex.Message);
                return new AccountResponseDto() { Message = AccountErrorMessage.UpdateAccountFailed };
            }
        }

        public async Task<ApiResponseModel<string>> ResetPasswordAsync(string userId)
        {
            var apiResponse = new ApiResponseModel<string>()
            {
                Errors = new List<string>(),
                Message = AccountErrorMessage.ResetAccountPasswordFailed,
            };

            try
            {
                var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
                var currentUser = await _userRepository.GetUserWithRoleByIdAsync(currentUserId);
                var targetUser = await _userRepository.GetUserWithRoleByIdAsync(userId);

                if (currentUser == null)
                {
                    apiResponse.Errors.Add(AccountErrorMessage.NotFoundCurrentAccount);
                }

                if (targetUser == null)
                {
                    apiResponse.Errors.Add(AccountErrorMessage.NotFoundTargetAccount);
                }

                if (currentUser != null && targetUser != null)
                {
                    await HasPermissionToReset(currentUser, targetUser);

                    string newPassword = GenerateRandomPassword();

                    Console.WriteLine($"NEW PASSWORD:  {newPassword}");

                    targetUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

                    await _userRepository.UpdateUserAsync(targetUser);

                    await SendForgotPasswordTokenMailAsync(targetUser, currentUser, newPassword);

                    apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                    apiResponse.Message = AccountSuccessMessage.UpdateAccountSuccess;
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessEx)
            {
                apiResponse.Errors.Add(unauthorizedAccessEx.Message);
            }
            catch (Exception ex)
            {
                apiResponse.Errors.Add(ex.Message);
            }
            return apiResponse;
        }

        private async Task HasPermissionToReset(User currentUser, User targetUser)
        {
            if (currentUser.RoleCode == RoleCodeConstant.PresidentCode && targetUser.RoleCode != RoleCodeConstant.ManagerCode)
            {
                throw new UnauthorizedAccessException(AccountErrorMessage.PresidentDoNotHavePermissionToReset);
            }

            if (currentUser.RoleCode == RoleCodeConstant.ManagerCode)
            {
                if (targetUser.RoleCode == RoleCodeConstant.CoachCode && targetUser.Coach?.TeamId != currentUser.Manager?.TeamId)
                {
                    throw new UnauthorizedAccessException(AccountErrorMessage.ManagerDoNotHavePermissionToResetCoach);
                }

                if (targetUser.RoleCode == RoleCodeConstant.PlayerCode && targetUser.Player?.TeamId != currentUser.Manager?.TeamId)
                {
                    throw new UnauthorizedAccessException(AccountErrorMessage.ManagerDoNotHavePermissionToResetPlayer);
                }

                if (targetUser.RoleCode == RoleCodeConstant.ParentCode)
                {
                    var player = await _playerRepository.GetPlayerByParentIdAsync(targetUser.UserId);
                    if (player?.TeamId != currentUser.Manager?.TeamId)
                    {
                        throw new UnauthorizedAccessException(AccountErrorMessage.ManagerDoNotHavePermissionToResetParent);
                    }
                }
            }
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // To send new password to user 
        private async Task SendForgotPasswordTokenMailAsync(User targetUser, User currentUser, string newPassword)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.ResetPasswordSuccess);
            if (mailTemplate != null)
            {
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{USER_FULLNAME}}", targetUser.Fullname)
                    .Replace("{{CHANGE_DATE}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                    .Replace("{{ACTION_USERNAME}}", currentUser.Username)
                    .Replace("{{ACTION_FULLNAME}}", currentUser.Fullname)
                    .Replace("{{NEW_PASSWORD}}", newPassword);

                var t = new Thread(() => _emailHelper.SendEmailMultiThread(targetUser.Email, mailTemplate.TemplateTitle, mailTemplate.Content));
                t.Start();
            }
        }
    }
}
