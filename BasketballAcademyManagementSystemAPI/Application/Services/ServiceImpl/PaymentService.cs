using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using log4net;
using Microsoft.VisualBasic;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAuthService _authService;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISendMailService _sendEmailService;
        private static readonly ILog _logger = LogManager.GetLogger(LogConstant.LogName.PaymentManagementFeature);


        public PaymentService(IPaymentRepository paymentRepository, IAuthService authService, ITeamRepository teamRepository, IPlayerRepository playerRepository, ISendMailService sendMailService)
        {
            _paymentRepository = paymentRepository;
            _authService = authService;
            _playerRepository = playerRepository;
            _sendEmailService = sendMailService;
        }

        public async Task<ApiMessageModelV2<PagedResponseDto<PaymentDto>>> GetPayments(PaymentFilterDto filter)
        {
            _logger.Info($"[GetPayments] Bắt đầu lấy lịch sử thanh toán ");

            // 1. Lấy thông tin người dùng đang đăng nhập
            dynamic user = await _authService.GetCurrentLoggedInUserInformationAsync();
            
            //cho 1 player
            if (user.RoleCode == RoleCodeConstant.PlayerCode)
            {
                filter.UserId = user.UserId;
            }
            //cho 1 parent 
            if (user.RoleCode == RoleCodeConstant.ParentCode)
            {
                await GetPaymentOfMyChild(filter);
            }
            //cho 1 team (cái này sẽ dùng cho những role manager or coach) cái này khó quá đang cân nhắc làm nên đành tách hàm
            //else if (user.RoleCode == RoleCodeConstant.ManagerCode || user.RoleCode == RoleCodeConstant.CoachCode)
            //{
            //}

            // 2. Lấy lịch sử thanh toán theo userId với phân trang
            var paymentPaging = await _paymentRepository.GetPaymentAsync(filter);

            if (paymentPaging.TotalRecords == 0)
            {
                _logger.Warn($"[GetPayments] Không tìm thấy dữ liệu thanh toán ");
                return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Bạn chưa có dữ liệu thanh toán nào",
                    Data = null
                };
            }
            else
            {
                _logger.Info($"[GetPayments] Lấy thành công {paymentPaging.TotalRecords} bản ghi thanh toán");
            }

            return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = "Lấy lịch sử thanh toán thành công",
                Data = paymentPaging
            };
        }

        public async Task<ApiMessageModelV2<PagedResponseDto<PaymentDto>>> GetPaymentOfMyChild(PaymentFilterDto filter)
        {
            try{
                _logger.Info($"[GetPaymentOfMyChild] Bắt đầu lấy thanh toán cho các con");

                // 1. Lấy thông tin người dùng đang đăng nhập
                dynamic user = await _authService.GetCurrentLoggedInUserInformationAsync();
                List<string> playerIds = new List<string>();
                //cho 1 parent 
                if (user.RoleCode == RoleCodeConstant.ParentCode)
                {
                    //lấy list userId của con parent đang đăng nhập
                    playerIds = await _playerRepository.GetPlayerUserIdsByParentIdAsync(user.UserId);
                }


                // 2. Lấy lịch sử thanh toán theo userId với phân trang
                var paymentPaging = await _paymentRepository.GetPaymentByListUserIdAsync(filter, playerIds);

                if (paymentPaging.TotalRecords == 0)
                {
                    return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
                    {
                        Status = ApiResponseStatusConstant.SuccessStatus,
                        Message = "Các con của bạn chưa có thanh toán nào",
                        Data = null
                    };
                }
                else
                {
                    _logger.Info($"[GetPaymentOfMyChild] Lấy thành công {paymentPaging.TotalRecords} bản ghi");
                }

                return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Lấy lịch sử thanh toán thành công",
                    Data = paymentPaging
                };
            }
            catch(Exception e) {  
                _logger.Error(e);
                return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Error in Get payment of my child",
                };
            }
        }

        public async Task<ApiMessageModelV2<PagedResponseDto<PaymentDto>>> GetPaymentsOfATeam(PaymentFilterDto filter, string teamId)
        {
            try
            {
                _logger.Info($"[GetPaymentsOfATeam] Lấy thanh toán của teamId: {teamId}");
                // Validate teamId
                if (string.IsNullOrEmpty(teamId))
                {
                    _logger.Warn("[GetPaymentsOfATeam] TeamId bị null hoặc rỗng.");
                    return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "TeamId không hợp lệ",
                        Data = null
                    };
                }

                var result = await _paymentRepository.GetPaymentByTeamIdAsync(filter, teamId);

                return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Lấy lịch sử thanh toán của đội thành công",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"[GetPaymentsOfATeam] Lỗi: {ex.Message}", ex);
                return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Đã xảy ra lỗi khi lấy lịch sử thanh toán",
                    Data = null
                };
            }
        }

        //cho tất cả nhưng đã phân role nếu role gì thì báo lỗi  dựa theo role đó
        public async Task<ApiMessageModelV2<PaymentDetailDto>> GetPaymentDetail(MyDetailPaymentRequestDto request)
        {
            // 1. Lấy chi tiết thanh toán từ repository
            var paymentDetail = await _paymentRepository.GetPaymentDetailAsync(request);

            // 2. Kiểm tra thanh toán có tồn tại không
            if (paymentDetail == null)
            {
                return new ApiMessageModelV2<PaymentDetailDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không tìm thấy thông tin thanh toán",
                    Data = null
                };
            }

            // 3. Xác minh thanh toán thuộc về người dùng hiện tại
            dynamic user = await _authService.GetCurrentLoggedInUserInformationAsync();
            if (user.RoleCode == RoleCodeConstant.PlayerCode)//nếu là player thì xem payment này có phải của nó không
            {
                if (paymentDetail.UserId != user.UserId)
                {
                    return new ApiMessageModelV2<PaymentDetailDto>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Bạn không có quyền xem thông tin thanh toán này",
                        Data = null
                    };
                }
            }
            //nếu là manager và coach thì xem payment này của thằng nào thuộc TeamFund nào team fund này thuộc Team nào và thằng đang đăng nhập có thuộc team đó không
            //nếu là parent thì xem paymentId này có thuộc của player mà player này là con của parentId đang đăng nhập
            else if (user.RoleCode == RoleCodeConstant.ManagerCode || user.RoleCode == RoleCodeConstant.CoachCode || user.RoleCode == RoleCodeConstant.ParentCode) 
            {
                if (!await _paymentRepository.HasAccessToPaymentDetailAsync(user.UserId, request.PaymentId, user.RoleCode))
                {
                    _logger.Info($"[GetPaymentDetail] Kiểm tra quyền truy cập thanh toán PaymentId: {request.PaymentId}, UserId: {user.UserId}, RoleCode: {user.RoleCode}");            
                    return new ApiMessageModelV2<PaymentDetailDto>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = (user.RoleCode == RoleCodeConstant.ParentCode ? "Đây không phải thông tin thanh toán của con của bạn" : "Đây không phải thông tin thanh toán của team bạn đảm nhận") + ". Bạn không có quyền xem thông tin thanh toán này",
                        Data = null
                    };
                }
            }
            //nếu là president thì có toàn quyền xem cái gì cũng được
            _logger.Info($"[GetPaymentDetail] Truy xuất thành công paymentId: {request.PaymentId} bởi UserId: {user.UserId}");
            // 4. Trả về chi tiết thanh toán
            return new ApiMessageModelV2<PaymentDetailDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = "Lấy thông tin thanh toán thành công",
                Data = paymentDetail
            };
        }
        
        public async Task<ApiMessageModelV2<PagedResponseDto<PaymentDto>>> GetListPlayerNotPaymentYet(PaymentFilterDto filter)
        {
            _logger.Info("[GetListPlayerNotPaymentYet] Lấy danh sách người chơi chưa thanh toán");


            filter.UserId = null;
            filter.Status = PaymentStatusConstant.NOT_PAID;

            // 2. Lấy danh sách người chơi chưa thanh toán với phân trang
            var paymentPaging = await _paymentRepository.GetPaymentAsync(filter);

            _logger.Info($"[GetListPlayerNotPaymentYet] Trả về {paymentPaging.TotalRecords} kết quả");
            return new ApiMessageModelV2<PagedResponseDto<PaymentDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = "Lấy danh sách người chơi chưa thanh toán thành công",
                Data = paymentPaging
            };
        }

        public async Task SendDueDateReminderEmailsAsync()
        {
            _logger.Info("\n[SendDueDateReminderEmailsAsync] Bắt đầu quá trình quét payment để email nhắc nhở thanh toán\n");
            var today = DateTime.Today;
            var reminderDays = new[] { 5, 3, 1 };

            foreach (var days in reminderDays)
            {
                var targetDate = today.AddDays(days);
                var payments = await _paymentRepository.GetPaymentsDueOnDateAsync(targetDate);

                foreach (var payment in payments)
                {
                    var player = payment.User;
                    var email = player?.User?.Email;
                    if (!string.IsNullOrEmpty(email))
                    {
                        _logger.Info("\n[SendDueDateReminderEmailsAsync] gửi email nhắc nhở thanh toán\n");
                        await _sendEmailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.ReminderPayment, 
                            email, 
                            new
                            {
                                FullName = player?.User.Fullname != null ? player.User.Fullname.ToString() : "Bạn",
                                DayLeft = days.ToString(),
                                TeamFundDescription = payment.TeamFund?.Description !=null ? payment.TeamFund?.Description.ToString() : "Quỹ đội tháng này",
                                DueDate = payment.DueDate.ToString()
                            });
                    }
                }
            }
        }

    }
}