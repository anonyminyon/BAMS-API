using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using log4net;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class ManagerRegistrationService : IManagerRegistrationService
    {
        private readonly IMapper _mapper;
        private readonly IManagerRegistrationRepository _managerRegistrationRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly EmailHelper _emailHelper;
        private readonly IEmailVerificationRepository _emailVerificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISendMailService _sendMailService;
        private readonly IOtpService _otpService;
        private readonly IPlayerRegistrationRepository _playerRegistrationRepository;
        private readonly IMemberRegistrationSessionRepository _memberRegistrationSessionRepository;
        private static readonly ILog _logger = LogManager.GetLogger(LogConstant.LogName.ManagerRegistrationManagementFeature);
        private readonly AccountGenerateHelper _accountGenerateHelper;
        public ManagerRegistrationService(IMapper mapper, IManagerRegistrationRepository managerRegistrationRepository,
            IManagerRepository managerRepository, IMailTemplateRepository mailTemplateRepository, EmailHelper emailHelper,
            IEmailVerificationRepository emailVerificationRepository, IUserRepository userRepository, ISendMailService sendMailService,
            IOtpService otpService, IPlayerRegistrationRepository playerRegistrationRepository, IMemberRegistrationSessionRepository memberRegistrationSessionRepository, AccountGenerateHelper accountGenerateHelper)
        {
            _mapper = mapper;
            _managerRegistrationRepository = managerRegistrationRepository;
            _managerRepository = managerRepository;
            _mailTemplateRepository = mailTemplateRepository;
            _emailHelper = emailHelper;
            _emailVerificationRepository = emailVerificationRepository;
            _userRepository = userRepository;
            _sendMailService = sendMailService;
            _otpService = otpService;
            _playerRegistrationRepository = playerRegistrationRepository;
            _memberRegistrationSessionRepository = memberRegistrationSessionRepository;
            _accountGenerateHelper = accountGenerateHelper;
        }

        //khi chưa verify otp thì không được lưu
        public async Task<ApiMessageModelV2<ManagerRegistrationDto>> RegisterManagerAsync(ManagerRegistrationDto managerRegistrationDto)
        {
            // 1️ Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(managerRegistrationDto.Email) || string.IsNullOrEmpty(managerRegistrationDto.PhoneNumber))
            {
                return new ApiMessageModelV2<ManagerRegistrationDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.EmailOrPhoneNumberCanNotNull,
                    Data = managerRegistrationDto
                };
            }

            /*check mail đăng ký đã xác thực otp chưa nếu chưa trả về mã lỗi, những thông tin nhập trên form
            và để gọi api send otp code kèm redirect đến trang nhập otp*/
            if (!await _emailVerificationRepository.IsEmailVerified(managerRegistrationDto.Email))
            {
                _logger.Warn($"Email {managerRegistrationDto.Email} chưa xác thực OTP.");
                return new ApiMessageModelV2<ManagerRegistrationDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.EmailNotVerifyOtpYet,
                    Data = managerRegistrationDto
                };
            }
            _logger.Info($"Bắt đầu kiểm tra email cho đăng ký: {managerRegistrationDto.Email}, {managerRegistrationDto.PhoneNumber}");
            /* 2️ Kiểm tra email */
            var errors = await ValidateRegistrationEmail(
                managerRegistrationDto.Email, managerRegistrationDto.MemberRegistrationSessionId);
            if (errors.Any())
            {
                _logger.Warn($"Email đã tồn tại: {managerRegistrationDto.Email}");
                return new ApiMessageModelV2<ManagerRegistrationDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.EmailOrPhoneNumberExists,
                    Data = managerRegistrationDto,
                    Errors = errors
                };
            }

            // 3 Chuyển đổi DTO sang Entity và bỏ qua thuộc tính virtual
            var entity = _mapper.Map<ManagerRegistration>(managerRegistrationDto);
            entity.Status = RegistrationStatusConstant.PENDING;
            entity.SubmitedDate = DateTime.UtcNow;
            await _managerRegistrationRepository.AddNewManagerRegistrationAsync(entity);
            _logger.Info($"Thêm đăng ký mới cho quản lý: {managerRegistrationDto.Email}");
            //gửi mail thông báo đăng ký thành công
            await _sendMailService.SendMailByMailTemplateIdAsync(
                    MailTemplateConstant.SendFormRegistrationSuccess,
                    entity.Email,
                    new
                    {
                        FullName = entity.FullName,
                        RoleCode = RoleCodeConstant.ManagerCode,
                    });
            _logger.Info($"Gửi email xác nhận đăng ký thành công cho: {entity.Email}");
            // 4️ Trả về response thành công
            return new ApiMessageModelV2<ManagerRegistrationDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerRegistrationMessage.Success.ManagerRegistrationSuccess
            };
        }

        public async Task<ApiMessageModelV2<ManagerRegistrationDto>> UpdateRegisterManagerAsync(ManagerRegistrationDto managerRegistrationDto)
        {
            // 1️ Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(managerRegistrationDto.Email) || string.IsNullOrEmpty(managerRegistrationDto.PhoneNumber))
            {
                return new ApiMessageModelV2<ManagerRegistrationDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.EmailOrPhoneNumberCanNotNull,
                    Data = managerRegistrationDto
                };
            }

            /*
            // 2️ Kiểm tra email không check phần này nữa vì nó đã được validate ở phần otp
            var errors =
                await ValidateRegistrationEmail(
                managerRegistrationDto.Email,
                managerRegistrationDto.MemberRegistrationSessionId);
            if (errors.Any())
            {
                return new ApiMessageModelV2<ManagerRegistrationDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.EmailOrPhoneNumberExists,
                    Data = managerRegistrationDto,
                    Errors = errors
                };
            }
            */
            /*
            //check if email is a email of a registration in this member registration session and pending
            var oldManagerRegistration =
                await _managerRegistrationRepository.IsEmailExistsAndPendingAndInMemberRegistrationSessionAsync(null,
                managerRegistrationDto.Email, managerRegistrationDto.MemberRegistrationSessionId);
            //if yes return that ManagerRegistration to Data
            if (oldManagerRegistration != null)
            {
                //return true
                return new ApiMessageModelV2<ManagerRegistrationDto>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = ManagerRegistrationMessage.Success.EmailRegistrationValidAndSendOtpAndUpdate,
                    Data = _mapper.Map<ManagerRegistrationDto>(oldManagerRegistration)
                };
            }
            */

            // 3 Chuyển đổi DTO sang Entity và bỏ qua thuộc tính virtual
            var entity = _mapper.Map<ManagerRegistration>(managerRegistrationDto);
            entity.Status = RegistrationStatusConstant.PENDING;
            entity.SubmitedDate = DateTime.Now;

            // 4 Cập nhật thông tin đăng ký
            await _managerRegistrationRepository.UpdateManagerRegistrationAsync(entity);
            _logger.Info($"Cập nhật thông tin đăng ký cho: {managerRegistrationDto.Email}");
            // 5 Trả về response thành công
            return new ApiMessageModelV2<ManagerRegistrationDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerRegistrationMessage.Success.ManagerRegistrationUpdateSuccess,
                Data = managerRegistrationDto
            };
        }

        public async Task<PagedResponseDto<ManagerRegistrationDto>> GetAllRegisterManager(ManagerRegistrationFilterDto filter)
        {
            return await _managerRegistrationRepository.GetAllManagerRegistrations(filter);
        }

        public async Task<ApiMessageModelV2<UserAccountDto<ManagerDto>>> ApproveRegistrationAsync(int managerRegistrationId)
        {
            _logger.Info($"Duyệt đăng ký với ID: {managerRegistrationId}");
            //get info in manager registration 
            ManagerRegistration mr = await _managerRegistrationRepository.GetManagerRegistrationByID(managerRegistrationId);
            if (mr == null)
            {
                return new ApiMessageModelV2<UserAccountDto<ManagerDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.ManagerRegistrationNotFound,
                    Data = null,
                    Errors = new Dictionary<string, string>() { { CommonMessage.Key.ErrorGeneral, ManagerRegistrationMessage.Error.ManagerRegistrationNotFound } }
                };
            }

            //create data for table User 
            string newUserId = Guid.NewGuid().ToString();
            var generateUsername = _accountGenerateHelper.GetUniqueUsername(mr.FullName);
            var generatePassword = _accountGenerateHelper.GeneratePassword();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(generatePassword);

            User user = new User
            {
                UserId = newUserId,
                Username = generateUsername,
                Password = hashedPassword,
                Fullname = mr.FullName,
                Email = mr.Email,
                Phone = mr.PhoneNumber,
                Address = ManagerConstant.TEMP_VALUE_NOT_UPDATE_STRING,
                RoleCode = RoleCodeConstant.ManagerCode,
                CreatedAt = DateTime.Now,
                IsEnable = true
            };

            Manager manager = new Manager
            {
                UserId = newUserId,
                BankName = ManagerConstant.TEMP_VALUE_NOT_UPDATE_STRING,
                BankAccountNumber = ManagerConstant.TEMP_VALUE_NOT_UPDATE_STRING
            };

            try
            {
                await _managerRepository.AddManagerAsync(user, manager);
            }
            catch (Exception ex)
            {
                _logger.Error($"Lỗi khi tạo tài khoản: {ex.Message}");
                Console.WriteLine($"Error: {ex.Message}");
            }

            //==================================== if create success manager====================================
            // Fetch user with manager details from the repository
            var newUser = await _managerRepository.GetUserWithManagerDetailAsync(newUserId);

            // Validate if the user exists and has manager data
            if (newUser == null || newUser.Manager == null)
            {
                return new ApiMessageModelV2<UserAccountDto<ManagerDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.CreateUserAndManagerFailed,
                    Data = null,
                    Errors = new Dictionary<string, string>() { { CommonMessage.Key.ErrorGeneral, ManagerRegistrationMessage.Error.CreateUserAndManagerFailed } }
                };
            }

            // Map to DTO and return response
            var newManagerNotHaveTeamYet = _mapper.Map<UserAccountDto<ManagerDto>>(newUser);

            //send mail notify to user
            await _sendMailService.SendMailByMailTemplateIdAsync(
                MailTemplateConstant.ApproveManagerRegistration,
                newManagerNotHaveTeamYet.Email,
                new
                {
                    Fullname = newUser.Fullname,
                    Username = generateUsername,
                    Password = generatePassword,
                });
            _logger.Info($"Gửi email phê duyệt cho: {newManagerNotHaveTeamYet.Email}");
            //change status pending to approve in manager registration
            bool isUpdated = await _managerRegistrationRepository
                .ChangeStatusByManagerRegistrationID(managerRegistrationId, RegistrationStatusConstant.APPROVED);

            _logger.Info($"Cập nhật trạng thái duyệt cho đăng ký ID: {managerRegistrationId}");
            if (!isUpdated)
            {
                return new ApiMessageModelV2<UserAccountDto<ManagerDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.UpdateStatusFailed,
                    Data = null
                };
            }

            return new ApiMessageModelV2<UserAccountDto<ManagerDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerRegistrationMessage.Success.ManagerRegistrationApproveSuccess,
                Data = newManagerNotHaveTeamYet
            };
        }

        public async Task<ApiMessageModelV2<ManagerRegistrationDto>> RejectRegistrationAsync(int id)
        {
            _logger.Info($"Từ chối đăng ký quản lý ID: {id}");
            // Change status in manager registration to "Rejected"
            bool isUpdated = await _managerRegistrationRepository
                .ChangeStatusByManagerRegistrationID(id, RegistrationStatusConstant.REJECTED);

            if (!isUpdated)
            {
                _logger.Error($"Cập nhật trạng thái từ chối thất bại cho ID: {id}");
                return new ApiMessageModelV2<ManagerRegistrationDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.UpdateStatusFailed,
                    Data = null
                };
            }

            // Get the updated manager registration
            var managerRegistration = await _managerRegistrationRepository.GetManagerRegistrationByID(id);
            if (managerRegistration == null)
            {
                return new ApiMessageModelV2<ManagerRegistrationDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.ManagerRegistrationNotFound,
                    Data = null
                };
            }

            //send mail notify to user
            await _sendMailService.SendMailByMailTemplateIdAsync(
                MailTemplateConstant.RejectManagerRegistration,
                managerRegistration.Email,
                managerRegistration);
            _logger.Info($"Gửi email từ chối cho: {managerRegistration.Email}");
            // Map to DTO and return response
            var managerRegistrationDto = _mapper.Map<ManagerRegistrationDto>(managerRegistration);
            return new ApiMessageModelV2<ManagerRegistrationDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerRegistrationMessage.Success.ManagerRegistrationRejectSuccess,
                Data = managerRegistrationDto
            };
        }

        public async Task<ApiMessageModelV2<object>> ValidateInfoRegistrationAndSendOtpAsync(string email, int memberRegistrationSessionId)
        {
            if (email == null)
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.EmailRegistrationInValid,
                };
            }
            var errors = await ValidateRegistrationEmail(email, memberRegistrationSessionId);
            _logger.Info($"Xác minh email và gửi OTP: {email}, phiên đăng ký: {memberRegistrationSessionId}");
            if (errors.Any())
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerRegistrationMessage.Error.EmailRegistrationInValid,
                    Errors = errors
                };
            }

            // Send OTP for verification
            await _otpService.SendOtpCodeAsync(new DTOs.EmailVerification.SendOtpDto { Email = email, Purpose = EmailConstant.ManagerRegistrationForm });

            //check if email is a email of a registration in this member registration session and pending
            /*var oldManagerRegistration = await _managerRegistrationRepository.IsEmailExistsAndPendingAndInMemberRegistrationSessionAsync(null, email, memberRegistrationSessionId);
            //if yes return that ManagerRegistration to Data
            if (oldManagerRegistration != null)
            {
                //return true
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = ManagerRegistrationMessage.Success.EmailRegistrationValidAndSendOtpAndUpdate,
                    Data = _mapper.Map<ManagerRegistrationDto>(oldManagerRegistration)
                };
            }
            */
            //return true
            return new ApiMessageModelV2<object>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerRegistrationMessage.Success.EmailRegistrationValidAndSendOtp,
            };
        }

        private async Task<Dictionary<string, string>> ValidateRegistrationEmail(string email, int memberRegistrationSessionId)
        {
            var errors = new Dictionary<string, string>();

            // Check if email exists in Users table
            bool existsInUser = await _userRepository.EmailExistsAsync(email);
            if (existsInUser)
            {
                errors.Add(ManagerRegistrationMessage.Key.ErrorEmail, ManagerRegistrationMessage.Error.EmailExistsInUser);
                return errors;
            }

            // Check if session is disabled or has ended
            var session = await _memberRegistrationSessionRepository.GetByIdAsync(memberRegistrationSessionId);
            if (session == null || !(session.IsEnable) || session.EndDate < DateTime.UtcNow)
            {
                errors.Add("errorMemberRegistrationSessionId", MemberRegistrationSessionMessage.MemberRegistrationSessionErrorMessage.OutDateOrDisable);
            }

            // Check if email exists in PlayerRegistrations table and not rejected
            bool existsInPlayerRegistration = await _playerRegistrationRepository.IsEmailExistsAndPendingAsync(email);
            if (existsInPlayerRegistration)
            {
                errors.Add(ManagerRegistrationMessage.Key.ErrorEmail, ManagerRegistrationMessage.Error.EmailExistInPlayerRegistration);
                return errors;
            }

            // Check if email exists in ManagerRegistrations table but pending and in other member Registration Session
            bool existsInManagerRegistrationButOtherSesssion =
                await _managerRegistrationRepository.IsEmailExistsAndPendingAndNotInMemberRegistrationSessionAsync(email, memberRegistrationSessionId);
            if (existsInManagerRegistrationButOtherSesssion)
            {
                errors.Add(ManagerRegistrationMessage.Key.ErrorEmail, ManagerRegistrationMessage.Error.EmailRegisInOtherMemberRegisSessionAndPending);
                return errors;
            }

            //check if email exist in ManagerRegistrations table but reject and in member Registration Session
            bool existsInManagerRegistrationInSesssion =
                await _managerRegistrationRepository.IsEmailExistsAndRejectAndInMemberRegistrationSessionAsync(email, memberRegistrationSessionId);
            if (existsInManagerRegistrationInSesssion)
            {
                errors.Add(ManagerRegistrationMessage.Key.ErrorEmail, ManagerRegistrationMessage.Error.EmailRegisInMemberRegisSessionAndReject);
                return errors;
            }

            return errors;
        }
    }
}
