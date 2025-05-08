using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.SePay;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using System.Text.RegularExpressions;
using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using log4net;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Application.Repositories;
using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;


namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
	public class TeamFundService : ITeamFundService
	{
		private readonly ITeamFundRepository _teamFundRepository;
		private readonly IAuthService _authService;
		private readonly IGeneratePaymentService _generatePaymentService;
		private readonly IParentRepository _parentRepository;
		private readonly ISendMailService _sendMailService;
		private readonly IMapper _mapper;
		private readonly IPaymentRepository _paymentRepository;
		private readonly IUserTeamHistoryRepository _userTeamHistoryRepository;
        private readonly IPlayerRepository _playerRepository;
        private static readonly ILog _logger = LogManager.GetLogger(LogConstant.LogName.TeamFundManagementFeature);

        public TeamFundService(
            ITeamFundRepository teamFundRepository,
            IGeneratePaymentService generatePaymentService,
            IParentRepository parentRepository,
            ISendMailService sendMailService,
            IAuthService authService,
            IMapper mapper,
            IPlayerRepository playerRepository,
            IPaymentRepository paymentRepository,
			IUserTeamHistoryRepository userTeamHistoryRepository
			)
		{
			_teamFundRepository = teamFundRepository;
			_generatePaymentService = generatePaymentService;
			_authService = authService;
			_teamFundRepository = teamFundRepository;
			_parentRepository = parentRepository;
			_sendMailService = sendMailService;
			_paymentRepository = paymentRepository;
			_mapper = mapper;
            _playerRepository = playerRepository;
			_userTeamHistoryRepository = userTeamHistoryRepository;

		}

        //Tự động tạo quỹ đội hằng tháng từ ngày mùng 1 , đến ngày cuối của tháng
        public async Task AutoCreateTeamFundsAsync(bool skipDateCheck = true)
        {
            _logger.Info("AutoCreateTeamFundsAsync bắt đầu");
            var currentDate = DateOnly.FromDateTime(DateTime.Today);

            if (!skipDateCheck && currentDate.Day != 1) return;

            var startDate = new DateOnly(currentDate.Year, currentDate.Month, 1);
            var endDate = new DateOnly(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));

            try
            {
                var teams = await _teamFundRepository.GetAllTeamsAsync();

                foreach (var team in teams)
                {
                    try
                    {
                        var exists = await _teamFundRepository.TeamFundExistsAsync(team.TeamId, startDate, endDate);
                        if (exists) continue; _logger.Warn($"Quỹ đã tồn tại cho team {team.TeamId}, bỏ qua");

                        var fund = new TeamFund
                        {
                            TeamFundId = Guid.NewGuid().ToString(),
                            TeamId = team.TeamId,
                            StartDate = startDate,
                            EndDate = endDate,
                            Status = TeamFundStatusConstant.PENDING,
                            Description = $"Quỹ đội tháng {currentDate.Month}"
                        };

                        await _teamFundRepository.AddTeamFundAsync(fund);
                        _logger.Info($"Tạo quỹ mới cho team {team.TeamId}");
                    }
                    catch (Exception exTeam)
                    {
                        Console.WriteLine($"Lỗi khi tạo quỹ cho team {team.TeamId}: {exTeam.Message}");
                        _logger.Error($"Lỗi khi tạo quỹ cho team {team.TeamId}: {exTeam.Message}");
                    }
                }

                await _teamFundRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi chạy AutoCreateTeamFundsAsync: {ex.Message}");
            }
        }

        public async Task<ApiMessageModelV2<List<TeamFundListDto>>> GetTeamFundsAsync(TeamFundFilterDto filter)
        {
            try
            {
                var teamFunds = await _teamFundRepository.GetTeamFundsAsync(filter);

                // Kiểm tra nếu không có dữ liệu
                if (teamFunds == null || !teamFunds.Any())
                {
                    return new ApiMessageModelV2<List<TeamFundListDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không tìm thấy quỹ đội nào phù hợp",
                        Data = new List<TeamFundListDto>()
                    };
                }

                return new ApiMessageModelV2<List<TeamFundListDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Lấy danh sách quỹ đội thành công",
                    Data = teamFunds
                };
            }
            catch (Exception ex)
            {
                // Log error (optional)
                _logger.Error($"Lỗi khi lấy danh sách quỹ đội: {ex.Message}", ex);

                return new ApiMessageModelV2<List<TeamFundListDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Đã xảy ra lỗi trong quá trình lấy dữ liệu.",
                    Errors = new Dictionary<string, string> { { "exception", ex.Message } }
                };
            }
        }

        //Hàm cập nhật trạng thái thanh toán cho 1 payment theo services tự động sepay
        public async Task<ApiMessageModelV2<object>> UpdateStatusPaymentAutoSepay(SePayWebhookDto dataPaymentResponse)
        {
            string? paymentId = null;

            try
            {
                // Tách mã thanh toán: tìm đoạn YHBTXXXXX trong content với XXXXX 5 kí tự, tồn tại số và chữ hoa 
                var match = Regex.Match(dataPaymentResponse.Content ?? "", @"YHBT[A-Z0-9]+");
                if (!match.Success)
                {
                    return new ApiMessageModelV2<object>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không tìm thấy mã thanh toán hợp lệ trong nội dung."
                    };
                }

                paymentId = match.Value;
                var managerBankType = await _teamFundRepository.GetManagerPaymentByPaymentId(paymentId);
                //Nếu tài khoản manager chọn thanh toán thủ công thì ko cập nhật
                if (managerBankType.PaymentMethod == 0)
                {
                    return new ApiMessageModelV2<object>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Quản lí đã chọn chế độ thanh toán thủ công, ko cập nhật trạng thái đơn thanh toán"
                    };
                }
                // Kiểm tra payment có tồn tại không
                var existedPayment = await _teamFundRepository.AnyPaymentById(paymentId);

                // Nếu tồn tại payment thì cập nhật trạng thái thành "Đã thanh toán"
                var success = await _teamFundRepository.UpdatePaymentStatusAsync(paymentId, PaymentStatusConstant.PAID_CONFIRMED, null);
                if (!success)
                {
                    return new ApiMessageModelV2<object>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không tìm thấy payment hoặc cập nhật thất bại."
                    };
                }
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Cập nhật trạng thái thanh toán thành công."
                };
            }
            catch (Exception ex)
            {
                //cập nhật lại note thanh toán nếu ko thể xử lí thanh toán
                if (!string.IsNullOrEmpty(paymentId))
                {
                    // Chỉ cập nhật mô tả khi có paymentId
                    await _teamFundRepository.UpdatePaymentStatusAsync(paymentId, null, "Đã có lỗi khi thực hiện thanh toán. Vui lòng thử lại hoặc liên hệ CLB.");
                }
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Có lỗi xảy ra khi xử lý cập nhật thanh toán.",
                    Errors = new Dictionary<string, string>
                    {
                        { "exception", ex.Message }
                    }
                };
            }
        }
        public async Task<ApiMessageModelV2<object>> UpdateStatusPayment(UpdatePaymentStatusRequest request)
        {
            try
            {
                // Kiểm tra nếu PaymentId là rỗng
                if (string.IsNullOrWhiteSpace(request.PaymentId))
                {
                    return new ApiMessageModelV2<object>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Payment ID is required.",
                        Errors = new Dictionary<string, string>
                {
                    { "PaymentId", "Không tìm thấy đơn thanh toán này" }
                }
                    };
                }

                // Lấy thông tin đơn thanh toán từ database
                var payment = await _paymentRepository.GetPaymentByIdAsync(request.PaymentId);
                if (payment == null)
                {
                    return new ApiMessageModelV2<object>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Payment not found.",
                        Errors = new Dictionary<string, string>
                {
                    { "Payment", "Không tìm thấy đơn thanh toán này." }
                }
                    };
                }

                // Cập nhật trạng thái thanh toán và ngày thanh toán (PaidDate nếu có)
                payment.Status = request.Status;

                // Nếu có thông tin ngày thanh toán (PaidDate), cập nhật
                if (request.PaidDate.HasValue)
                {
                    payment.PaidDate = request.PaidDate.Value;
                }

                // Nếu có phương thức thanh toán (PaymentMethod), cập nhật
                if (request.PaymentMethod.HasValue)
                {
                    payment.PaymentMethod = request.PaymentMethod.Value;
                }

                // Lưu cập nhật vào database
                await _paymentRepository.UpdatePaymentAsync(payment);

                // Trả về kết quả thành công
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Trạng thái đơn thanh toán đã được cập nhật thành công."
                };
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu cần) và trả về thông báo lỗi chi tiết
                // Bạn có thể log lỗi ở đây (ví dụ: _logger.LogError(ex, "Error updating payment status"));

                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Đã có lỗi xảy ra trong quá trình xử lý.",
                    Errors = new Dictionary<string, string>
            {
                { "Exception", ex.Message }
            }
                };
            }
        }

        //president duyệt team fund
        public async Task<ApiMessageModelV2<string>> ApproveTeamFundAsync(string teamFundId)
        {
            try
            {
                var teamFund = await _teamFundRepository.GetTeamFundByIdAsync(teamFundId);
                if (teamFund == null || teamFund.Status != 0)
                {
                    return new ApiMessageModelV2<string>
                    {
                        Message = "Quỹ đội không tồn tại hoặc đã được duyệt"
                    };
                }

                // Lấy ra danh sách expenditures của 1 team cần duyệt theo userteamhistory theo thời gian của teamfund 
                var expenditures = await _teamFundRepository.GetExpendituresByTeamFundAndDateRangeAsync(teamFundId, teamFund.StartDate, teamFund.EndDate);

                // Kiểm tra nếu expenditures là null hoặc rỗng
                if (expenditures == null || !expenditures.Any())
                {
                    return new ApiMessageModelV2<string>
                    {
                        Message = "Không tìm thấy chi phí cho quỹ này"
                    };
                }

                // Lấy ra lịch sử userHistory của 1 đội theo teamId của teamfund 
                var userTeamHistory = await _teamFundRepository.GetUserTeamHistoriesByTeamIdAsync(teamFund.TeamId, teamFund.StartDate, teamFund.EndDate);

                // Kiểm tra nếu userTeamHistory là null hoặc rỗng
                if (userTeamHistory == null || !userTeamHistory.Any())
                {
                    return new ApiMessageModelV2<string>
                    {
                        Message = "Không tìm thấy lịch sử người dùng cho quỹ này"
                    };
                }
				// Lấy ra danh sách expenditures của 1 team cần duyệt theo userteamhistory theo thời gian của teamfund chứa 1()
				var expendituresStartWithOne = await _teamFundRepository.GetExpendituresByTeamFundAndDateRangeStartWithOneAsync(teamFundId, teamFund.StartDate, teamFund.EndDate);

				// Truy xuất những user phải trả khoản expenditure theo userId
				await SaveListUsersPaidForExpenditure(expendituresStartWithOne, userTeamHistory, teamFund);

			
				// Tạo các khoản chi cho payment và payment Items từ expenditure 
				await ExportExpenditureToPaymentInfo(teamFundId);

                // Cập nhật trạng thái quỹ
                teamFund.Status = TeamFundStatusConstant.APPROVED;
                teamFund.ApprovedAt = DateTime.Now;
                await _teamFundRepository.SaveChangesAsync();

                return new ApiMessageModelV2<string>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Duyệt quỹ thành công",
                    Data = teamFundId
                };
            }
            catch (Exception ex)
            {
                // Log exception here (optional)
                _logger.Error($"Lỗi khi duyệt quỹ: {ex.Message}", ex);

                // Trả về thông báo lỗi cho người dùng
                return new ApiMessageModelV2<string>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Đã xảy ra lỗi trong quá trình duyệt quỹ. Vui lòng thử lại sau.",
                    Errors = new Dictionary<string, string> { { "exception", ex.Message } }
                };
            }
        }

        /*
		  Tính toán và lưu các khoản thanh toán cho từng cầu thủ trong một kỳ quỹ (TeamFund), dựa trên dữ 
		  liệu từ bảng Expenditure — mỗi chi phí (Expenditure) được chia đều cho những người được liệt kê trong UsedByIds.
		 */
        private async Task ExportExpenditureToPaymentInfo(string teamFundId)
        {
            var expenditures = await _teamFundRepository.GetExpendituresByTeamFundIdAsync(teamFundId);
            var existingPayments = await _teamFundRepository.GetPaymentsByTeamFundIdAsync(teamFundId);

            var paymentDict = existingPayments.ToDictionary(p => p.UserId, p => p);

            foreach (var exp in expenditures)
            {
                var usedByIds = TryParseUsedUserIds(exp.UsedByUserId);

                if (usedByIds.Count == 0)
                    throw new Exception($"Expenditure '{exp.Name}' contains invalid UsedByUserId format.");

                var amountPerPlayer = Math.Round(exp.Amount / usedByIds.Count, 2);

                foreach (var userId in usedByIds)
                {
                    if (!paymentDict.TryGetValue(userId, out var payment))
                    {
                        payment = new Payment
                        {
                            PaymentId = await _generatePaymentService.GenerateUniquePaymentIdAsync(),
                            UserId = userId,
                            TeamFundId = teamFundId,
                            DueDate = DateTime.Now.AddDays(10),
                            Status = 0,
                            Note = "",
                            PaymentItems = new List<PaymentItem>()
                        };
                        paymentDict[userId] = payment;
                    }

                    payment.PaymentItems ??= new List<PaymentItem>();

                    bool alreadyExists = payment.PaymentItems.Any(item =>
                        item.PaidItemName == exp.Name &&
                        Math.Abs(item.Amount - amountPerPlayer) < 0.01m);

                    if (!alreadyExists)
                    {
                        var paymentItem = new PaymentItem
                        {
                            PaidItemName = exp.Name,
                            Amount = amountPerPlayer,
                            Note = exp.PayoutDate.HasValue
                                ? $"Thanh toán ngày {exp.PayoutDate.Value:dd/MM/yyyy} - tổng {exp.Amount:N0} VND"
                                : $"Khoản này đã được thanh toán - tổng {exp.Amount:N0} VND"
                        };
                        payment.PaymentItems.Add(paymentItem);
                    }
                }
            }

            foreach (var payment in paymentDict.Values)
            {
                var exists = existingPayments.Any(p => p.PaymentId == payment.PaymentId);
                if (!exists)
                    await _teamFundRepository.CreatePaymentWithItemsAsync(payment);
                else
                    await _teamFundRepository.UpdatePaymentWithItemsAsync(payment);
            }
        }

        private List<string> TryParseUsedUserIds(string usedByUserIdRaw)
        {
            if (string.IsNullOrEmpty(usedByUserIdRaw) || !usedByUserIdRaw.StartsWith("0(") && !usedByUserIdRaw.StartsWith("1("))
                return new List<string>();

            var start = usedByUserIdRaw.IndexOf('(') + 1;
            var end = usedByUserIdRaw.LastIndexOf(')');
            if (end <= start) return new List<string>();

            var content = usedByUserIdRaw.Substring(start, end - start);
            return content
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => id.Trim())
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .ToList();
        }

        private async Task SaveListUsersPaidForExpenditure(
            List<Expenditure> expenditures,
            List<UserTeamHistory> userTeamHistory,
            TeamFund teamFund
        )
        {
            //khai báo để chứa 1 expenditure tương ứng 1 list user phải chi khoản quỹ này
            Dictionary<string, List<string>> calculatePaymentList = new Dictionary<string, List<string>>();

            for (int i = 0; i < expenditures.Count; i++)
            {

                for (int j = 0; j < userTeamHistory.Count; j++)
                {
                    //Chuyển ngày join date sang DateTime
                    var userTeamHistoryJoinDate = userTeamHistory[j].JoinDate;

                    //Chuyển ngày join date sang DateTime
                    DateTime? userTeamHistoryLeftDate = userTeamHistory[j].LeftDate.HasValue
                               ? (userTeamHistory[j].LeftDate.Value) : null; ;
                    //Lấy ra payoutDate
                    var payoutDate = expenditures[i].PayoutDate.Value;
                    //convert ra teamFund bắt đầu từ 0h:0p:0s ngày đó
                    var teamFundStartDate = teamFund.StartDate.ToDateTime(TimeOnly.MinValue);
                    //convert ra teamFund endDate kết thúc là 23h59p 59s
                    var teamFundEndDate = teamFund.EndDate.ToDateTime(new TimeOnly(23, 59, 59));
                    /*
					============= trường hợp 1===========
					ngày join date trước ngày đầu tháng và leftDate = null => thằng này trong đội từ đầu tháng đến ngày cuối tháng
					*/
                    var isInRangeTeamHistory = payoutDate >= userTeamHistoryJoinDate && (payoutDate < teamFundEndDate);


                    if (userTeamHistoryJoinDate <= teamFundStartDate && userTeamHistoryLeftDate == null
                        && payoutDate >= userTeamHistoryJoinDate && (payoutDate < teamFundEndDate))
                    // trường hợp mà userTeamHistoryLeftDate == null thì cho paydate so sánh với ngày cuối tháng

                    {
                        if (!calculatePaymentList.ContainsKey(expenditures[i].ExpenditureId))
                        {
                            // Nếu chưa có expenditureId thì tạo mới List và thêm userId
                            calculatePaymentList[expenditures[i].ExpenditureId] = new List<string> { userTeamHistory[j].UserId };
                        }
                        else
                        {
                            // Nếu đã có thì chỉ cần thêm userId vào list
                            calculatePaymentList[expenditures[i].ExpenditureId].Add(userTeamHistory[j].UserId);
                        }
                    }

                    /*
					============= trường hợp 2===========
					ngày join đội trước ngày đầu tháng 	và leftDate != null => thằng này trong đội từ đầu tháng và rời ngày nào đó giữa tháng  
					*/
                    if (userTeamHistoryJoinDate <= teamFundStartDate && userTeamHistoryLeftDate != null
                        &&
                        // trường hợp mà userTeamHistoryLeftDate == null thì cho paydate so sánh với ngày cuối tháng
                        payoutDate >= userTeamHistoryJoinDate &&
                        (payoutDate < userTeamHistoryLeftDate))
                    {
                        if (!calculatePaymentList.ContainsKey(expenditures[i].ExpenditureId))
                        {
                            // Nếu chưa có expenditureId thì tạo mới List và thêm userId
                            calculatePaymentList[expenditures[i].ExpenditureId] = new List<string> { userTeamHistory[j].UserId };
                        }
                        else
                        {
                            // Nếu đã có thì chỉ cần thêm userId vào list
                            calculatePaymentList[expenditures[i].ExpenditureId].Add(userTeamHistory[j].UserId);
                        }

                    }

                    /*
					============= trường hợp 3===========
					ngày join đội  sau đầu tháng 	và leftDate == null => thằng này trong đội từ 1 ngày giữa tháng và rời cuối tháng  
					*/
                    if (userTeamHistoryJoinDate >= teamFundStartDate && userTeamHistoryLeftDate == null
                        &&
                        // trường hợp mà userTeamHistoryLeftDate == null thì cho paydate so sánh với ngày cuối tháng
                        payoutDate >= userTeamHistoryJoinDate &&
                        (payoutDate < teamFundEndDate))
                    {
                        if (!calculatePaymentList.ContainsKey(expenditures[i].ExpenditureId))
                        {
                            // Nếu chưa có expenditureId thì tạo mới List và thêm userId
                            calculatePaymentList[expenditures[i].ExpenditureId] = new List<string> { userTeamHistory[j].UserId };
                        }
                        else
                        {
                            // Nếu đã có thì chỉ cần thêm userId vào list
                            calculatePaymentList[expenditures[i].ExpenditureId].Add(userTeamHistory[j].UserId);
                        }
                    }
                    /*
					============= trường hợp 4===========
					ngày join đội  sau đầu tháng	và leftDate != null => thằng này trong đội từ 1 ngày giữa tháng và rời 1 ngày trước cuối tháng  
					*/
                    if (userTeamHistoryJoinDate >= teamFundStartDate && userTeamHistoryLeftDate != null
                        &&
                        // trường hợp mà userTeamHistoryLeftDate == null thì cho paydate so sánh với ngày cuối tháng
                        payoutDate >= userTeamHistoryJoinDate &&
                        (payoutDate < userTeamHistoryLeftDate))
                    {
                        if (!calculatePaymentList.ContainsKey(expenditures[i].ExpenditureId))
                        {
                            // Nếu chưa có expenditureId thì tạo mới List và thêm userId
                            calculatePaymentList[expenditures[i].ExpenditureId] = new List<string> { userTeamHistory[j].UserId };
                        }
                        else
                        {
                            // Nếu đã có thì chỉ cần thêm userId vào list
                            calculatePaymentList[expenditures[i].ExpenditureId].Add(userTeamHistory[j].UserId);
                        }
                    }
                }
            }
            await _teamFundRepository.SaveUsersPaidForExpendituresAsync(calculatePaymentList);

        }

        //Hàm lấy về user ( parent hoặc player) cần thanh toán dựa vào độ tuổi của học sinh
        private async Task<User> GetEmailPayment(User player)
        {

            if (player.DateOfBirth.HasValue)
            {
                var today = DateTime.Today;
                var dob = player.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);

                var age = today.Year - dob.Year;
                if (dob > today.AddYears(-age))
                    age--;

                if (age < 18)
                {
                    return await _parentRepository.GetUserParentByIdAsync(player.Player.ParentId);

                }
                else
                {
                    return player;
                }
            }
            else
            {
                return player;
            }

        }

        //Hàm lấy thông tin ngân hàng của manager đc chọn để quản lí thanh toán
        public async Task<ApiMessageModelV2<object>> GetManagerBankInfor(string paymentId)
        {
            if (string.IsNullOrWhiteSpace(paymentId))
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Payment ID is required.",
                    Errors = new Dictionary<string, string>
            {
                { "PaymentId", "Payment ID cannot be null or empty." }
            }
                };
            }

            var manager = await _teamFundRepository.GetManagerPaymentByPaymentId(paymentId);

            if (manager == null)
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không tìm thấy quản lí với đơn thanh toán này.",
                    Errors = new Dictionary<string, string>
            {
                { "Manager", "Không tìm thấy quản lí với đơn thanh toán này." }
            }
                };
            }

            var managerDto = new
            {
                UserId = manager.UserId,
                BankName = manager.BankName,
                BankAccountNumber = manager.BankAccountNumber,
                BankBinId = manager.BankBinId,
                TeamId = manager.TeamId,
                PaymentMethod = manager.PaymentMethod
            };

			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Lấy thông tin ngân hàng của người quản lí đội thành công",
				Data = managerDto
			};
		}

		// lấy ra danh sách player trong 1 team trc ngày truyền vào 
		public async Task<ApiMessageModelV2<List<PlayerExpenditureResponseDto>>> GetPlayerUserIdsByTeamAndDateAsync(string teamId, DateTime targetDate)
		{
			var response = new ApiMessageModelV2<List<PlayerExpenditureResponseDto>>
			{
				Errors = new Dictionary<string, string>()
			};

			if (string.IsNullOrWhiteSpace(teamId))
			{
				response.Status = ApiResponseStatusConstant.FailedStatus;
				response.Message = "TeamId không được để trống.";
				response.Errors.Add("TeamId", "Giá trị TeamId không hợp lệ.");
				return response;
			}

			if (targetDate == default)
			{
				response.Status = ApiResponseStatusConstant.FailedStatus;
				response.Message = "Ngày kiểm tra không hợp lệ.";
				response.Errors.Add("TargetDate", "Giá trị ngày không hợp lệ.");
				return response;
			}

			try
			{
				var userIds = await _userTeamHistoryRepository.GetUserIdsByTeamAndDateAsync(teamId, targetDate);

				if (userIds == null || !userIds.Any())
				{
					response.Status = ApiResponseStatusConstant.FailedStatus;
					response.Message = "Không tìm thấy cầu thủ nào trong đội vào thời điểm chỉ định.";
					return response;
				}

				response.Status = ApiResponseStatusConstant.SuccessStatus;
				response.Message = "Lấy danh sách cầu thủ thành công.";
				response.Data = userIds;
				return response;
			}
			catch (Exception ex)
			{
				response.Status = ApiResponseStatusConstant.FailedStatus;
				response.Message = "Đã xảy ra lỗi trong quá trình xử lý.";
				response.Errors.Add("Exception", ex.Message);
				return response;
			}
		}


        //Từ chối quản lí quỹ đội
        public async Task<ApiMessageModelV2<object>> RejectTeamFund(string teamFundId, string reasonReject)
        {
            try
            {
                var managerEmailByTeamFundId = await _teamFundRepository.GetManagerEmailByTeamFundId(teamFundId);

				_sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.RejectTeamFund, managerEmailByTeamFundId, reasonReject);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = $"Đã thông báo lí do từ chối quỹ đội tới email quản lí đội"
				};
			}
            catch (Exception ex) {

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Không tìm thấy quản lí với đơn thanh toán này.",
					Errors = new Dictionary<string, string>
			{
				{ "exception", "Đã có lỗi khi gửi email từ chối quỹ đội tới quản lí" }
			}
				};
			}
        }

        //=========================================phần expenditure của hiếu==================================================
        //Hàm tính toán lại payment và paymentItem mỗi khi update lại expenditure sau khoảng thời gian duyệt quỹ
        private async Task<ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>> ReCalculatePaymentWhenChangeExpenditure(string teamFundId, bool isMethodDeleteExpenditure)
        {
            _logger.Info($"Bắt đầu tái tính toán Payment từ Expenditure với teamFundId = {teamFundId}");
            // 2. Lấy thông tin quỹ
            var teamFund = await _teamFundRepository.GetTeamFundByIdAsync(teamFundId);
            
            if (teamFund == null || teamFund.Status == 1)
            {
                // 1. Xóa tất cả payment và paymentItems
                var isDeleteSuccess = await _teamFundRepository.DeletePaymentsByTeamFundIdAsync(teamFundId);
                if (!isDeleteSuccess)
                {
                    _logger.Error($"Không thể xóa các khoản thanh toán của quỹ {teamFundId}");
                    return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không thể xóa các đơn thanh toán hiện tại.",
                        Errors = new Dictionary<string, string> { { "ErrorDeletePayment", "Không thể xóa một vài đơn thanh toán hoặc dữ liệu liên quan" } }
                    };
                }

                // 3. Lấy danh sách khoản chi theo thời gian
                var expenditures = await _teamFundRepository.GetExpendituresByTeamFundAndDateRangeAsync(teamFundId, teamFund.StartDate, teamFund.EndDate);
                if (expenditures == null || !expenditures.Any())
                {
                    return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không tìm thấy khoản chi nào cho quỹ này.",
                        Errors = new Dictionary<string, string> { { "ExpenditureNotFound", "Danh sách khoản chi rỗng" } }
                    };
                }

                // 4. Lấy lịch sử thành viên trong đội để chia tiền
                var userTeamHistory = await _teamFundRepository.GetUserTeamHistoriesByTeamIdAsync(teamFund.TeamId, teamFund.StartDate, teamFund.EndDate);
                if (userTeamHistory == null || !userTeamHistory.Any())
                {
                    return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không tìm thấy lịch sử thành viên của đội.",
                        Errors = new Dictionary<string, string> { { "UserTeamHistoryMissing", "Không có thành viên nào trong khoảng thời gian hoạt động của quỹ" } }
                    };
                }
                var expendituresStartWithOne = await _teamFundRepository.GetExpendituresByTeamFundAndDateRangeStartWithOneAsync(teamFundId, teamFund.StartDate, teamFund.EndDate);

                // 5. Gán danh sách UserId cho mỗi Expenditure cần chia tiền
                await SaveListUsersPaidForExpenditure(expendituresStartWithOne, userTeamHistory, teamFund);

                // 6. Tạo lại các Payment và PaymentItem từ dữ liệu Expenditure mới
                await ExportExpenditureToPaymentInfo(teamFundId);

                _logger.Info($"Hoàn tất tái tính toán Payment cho quỹ {teamFundId}");
            }
            
            return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = "Tái tính toán các khoản thanh toán thành công.",
                Data = Enumerable.Empty<UpdateExpenditureDto>() // Có thể thay bằng dữ liệu thực nếu cần trả về
            };
        }

        public async Task<ApiMessageModelV2<IEnumerable<ExpenditureDto>>> AddExpendituresAsync(
    IEnumerable<CreateExpenditureHasListUserIdDto> expenditures,
    string teamFundId)
        {
            try
            {
                dynamic userLoggedDynamic = await _authService.GetCurrentLoggedInUserInformationAsync();
                var entities = new List<Expenditure>();
                var duplicateWarning = new Dictionary<string, string>();
                var duplicateError = new Dictionary<string, string>();
                int numberWarning = 0;

                _logger.Info($"Bắt đầu thêm mới expenditures cho teamFundId: {teamFundId}");

                var existingExpenditures = await _teamFundRepository.GetExpendituresByTeamFundIdAsync(teamFundId);

                foreach (var newExpenditure in expenditures)
                {
                    var tempDate = newExpenditure.PayoutDate.Value;
                    var isPayoutValid = await _teamFundRepository
                        .IsPayoutDateValidForAnyUserInTeamFundAsync(tempDate, teamFundId);

                    if (!isPayoutValid)
                    {
                        _logger.Warn($"Ngày chi không hợp lệ với bất kỳ người dùng nào trong team cho khoản chi: {newExpenditure.Name}");
                        return new ApiMessageModelV2<IEnumerable<ExpenditureDto>>
                        {
                            Status = ApiResponseStatusConstant.FailedStatus,
                            Message = $"Không thể tạo khoản chi. Ngày chi '{newExpenditure.PayoutDate:yyyy-MM-dd}' đội không có cầu thủ nào.",
                            Errors = new Dictionary<string, string>
                    {
                        { "InvalidPayoutDate", $"Ngày chi không hợp lệ với khoản chi '{newExpenditure.Name}'." }
                    }
                        };
                    }

                    foreach (var existingExpenditure in existingExpenditures)
                    {
                        double similarity = CalculateStringSimilarity(newExpenditure.Name, existingExpenditure.Name);

                        if (similarity == 1 && Math.Abs(newExpenditure.Amount - existingExpenditure.Amount) < 0.01m && newExpenditure.PayoutDate == existingExpenditure.PayoutDate)
                        {
                            duplicateError.Add(
                                key: $"Error_{numberWarning++}",
                                value: $"Lỗi: Khoản chi '{newExpenditure.Name}' (giống {similarity:P2}) với '{existingExpenditure.Name}' trong hệ thống."
                            );
                        }

                        if (similarity > 0.7 && Math.Abs(newExpenditure.Amount - existingExpenditure.Amount) < 0.01m)
                        {
                            duplicateWarning.Add(
                                key: $"Warning_{numberWarning++}",
                                value: $"Cảnh báo: Khoản chi '{newExpenditure.Name}' (giống {similarity:P2}) với '{existingExpenditure.Name}' trong hệ thống."
                            );
                        }
                    }
                }

                if (duplicateError.Count > 0)
                {
                    _logger.Warn($"Thêm expenditures thất bại vì có {duplicateError.Count} khoản chi trùng 100%.");
                    return new ApiMessageModelV2<IEnumerable<ExpenditureDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Lỗi cập nhập trùng khoản chi.",
                        Errors = duplicateError
                    };
                }

                foreach (var expenditure in expenditures)
                {
                    string? usedBy = null;
                    if (expenditure.UserIds != null && expenditure.UserIds.Any())
                    {
                        usedBy = $"0({string.Join(",", expenditure.UserIds)})";
                    }
                    else
                    {
                        return new ApiMessageModelV2<IEnumerable<ExpenditureDto>>
                        {
                            Status = ApiResponseStatusConstant.FailedStatus,
                            Message = "Vui lòng chọn cầu thủ cho khoản chi.",
                            Errors = duplicateWarning.Any() ? duplicateWarning : null
                        };
                    }

                    entities.Add(new Expenditure
                    {
                        ExpenditureId = Guid.NewGuid().ToString(),
                        TeamFundId = teamFundId,
                        ByManagerId = userLoggedDynamic.UserId,
                        Name = expenditure.Name,
                        Amount = expenditure.Amount,
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        PayoutDate = expenditure.PayoutDate?.Date.AddDays(1).AddSeconds(-1),
                        UsedByUserId = usedBy
                    });
                }

                    await _teamFundRepository.AddExpendituresAsync(entities);
                    _logger.Info($"Thêm thành công {entities.Count} expenditures vào teamFundId: {teamFundId}");

                    /*after update or add new expenditure and team fund still in pending 3 day 
                     * so code must udpate data just update from expenditure to payment and payment item*/
                    var reCalculateResult = await ReCalculatePaymentWhenChangeExpenditure(teamFundId,false);
                    if (reCalculateResult.Status == ApiResponseStatusConstant.FailedStatus)
                    {
                        return new ApiMessageModelV2<IEnumerable<ExpenditureDto>>
                        {
                            Status = ApiResponseStatusConstant.FailedStatus,
                            Message = "Có lỗi xảy ra khi tái tính toán các khoản thanh toán.",
                            Errors = reCalculateResult.Errors
                        };
                    }

                return new ApiMessageModelV2<IEnumerable<ExpenditureDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Thêm mới khoản chi thành công.",
                    Errors = duplicateWarning.Any() ? duplicateWarning : null
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"Lỗi hệ thống khi thêm expenditures: {ex.Message}", ex);
                return new ApiMessageModelV2<IEnumerable<ExpenditureDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Đã xảy ra lỗi hệ thống. Vui lòng thử lại sau.",
                    Errors = new Dictionary<string, string> { { "ServerError", ex.Message } }
                };
            }
        }

        public async Task<ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>> UpdateExpendituresAsync(
    IEnumerable<UpdateExpenditureHasListUserIdDto> expenditures,
    string teamFundId)
        {
            try
            {
                dynamic userLoggedDynamic = await _authService.GetCurrentLoggedInUserInformationAsync();
                var entities = new List<Expenditure>();
                var duplicateWarning = new Dictionary<string, string>();
                var duplicateError = new Dictionary<string, string>();
                int numberWarning = 0;

                _logger.Info($"Bắt đầu cập nhật khoản chi cho teamFundId: {teamFundId}");

                var existingExpenditures = await _teamFundRepository.GetExpendituresByTeamFundIdAsync(teamFundId);

                foreach (var updatedExpenditure in expenditures)
                {
                    var targetExpenditure = existingExpenditures.FirstOrDefault(e =>
                        e.ExpenditureId == updatedExpenditure.Id);

                    if (targetExpenditure == null)
                    {
                        duplicateError.Add($"Error_{numberWarning++}", $"Không tìm thấy khoản chi cần cập nhật: {updatedExpenditure.Name}");
                        continue;
                    }
                    var tempDate = updatedExpenditure.PayoutDate.Value;
                    var isPayoutValid = await _teamFundRepository
                        .IsPayoutDateValidForAnyUserInTeamFundAsync(tempDate, teamFundId);

                    if (!isPayoutValid)
                    {
                        _logger.Warn($"Ngày chi không hợp lệ với bất kỳ người dùng nào trong team cho khoản chi: {updatedExpenditure.Name}");
                        return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                        {
                            Status = ApiResponseStatusConstant.FailedStatus,
                            Message = $"Không thể cập nhật expenditures. PayoutDate '{updatedExpenditure.PayoutDate:yyyy-MM-dd}' không thuộc khoảng thời gian hoạt động của bất kỳ user nào trong team.",
                            Errors = new Dictionary<string, string>
                    {
                        { "InvalidPayoutDate", $"PayoutDate không hợp lệ với khoản chi '{updatedExpenditure.Name}'." }
                    }
                        };
                    }

                    foreach (var existingExpenditure in existingExpenditures)
                    {
                        if (existingExpenditure.ExpenditureId == targetExpenditure.ExpenditureId) continue;

                        double similarity = CalculateStringSimilarity(updatedExpenditure.Name, existingExpenditure.Name);
                        if (similarity == 1 && Math.Abs(updatedExpenditure.Amount - existingExpenditure.Amount) < 0.01m && updatedExpenditure.PayoutDate == existingExpenditure.PayoutDate)
                        {
                            duplicateError.Add($"Error_{numberWarning++}", $"Lỗi: Khoản chi '{updatedExpenditure.Name}' (giống {similarity:P2}) với '{existingExpenditure.Name}' trong hệ thống.");
                        }
                        else if (similarity > 0.7 && Math.Abs(updatedExpenditure.Amount - existingExpenditure.Amount) < 0.01m)
                        {
                            duplicateWarning.Add($"Warning_{numberWarning++}", $"Cảnh báo: Khoản chi '{updatedExpenditure.Name}' (giống {similarity:P2}) với '{existingExpenditure.Name}' trong hệ thống.");
                        }
                    }

                    targetExpenditure.Name = updatedExpenditure.Name;
                    targetExpenditure.Amount = updatedExpenditure.Amount;
                    targetExpenditure.PayoutDate = updatedExpenditure.PayoutDate;
                    targetExpenditure.ByManagerId = userLoggedDynamic.UserId;
                    targetExpenditure.Date = DateOnly.FromDateTime(DateTime.Now);

                    // Xử lý UsedByUserId theo AllowToEditPlayer
                    if (updatedExpenditure.UserIds != null && updatedExpenditure.UserIds.Any())
                    {
                        targetExpenditure.UsedByUserId = $"0({string.Join(",", updatedExpenditure.UserIds)})";
                    }
                    else
                    {
                        return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                        {
                            Status = ApiResponseStatusConstant.FailedStatus,
                            Message = "Vui lòng chọn cầu thủ phải trả khoản chi đang cập nhật.",
                            Errors = duplicateWarning.Any() ? duplicateWarning : null
                        };
                    }

                    entities.Add(targetExpenditure);
                }

                if (duplicateError.Count > 0)
                {
                    _logger.Warn($"Cập nhật khoản chi thất bại vì có {duplicateError.Count} khoản chi không hợp lệ.");
                    return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Lỗi cập nhập trùng khoản chi hoặc không tìm thấy dữ liệu.",
                        Errors = duplicateError
                    };
                }

                    await _teamFundRepository.UpdateExpendituresAsync(entities);
                    _logger.Info($"Cập nhật thành công {entities.Count} khoản chi cho teamFundId: {teamFundId}");
                    /*after update or add new expenditure and team fund still in pending 3 day 
                     * so code must udpate data just update from expenditure to payment and payment item*/
                    var reCalculateResult = await ReCalculatePaymentWhenChangeExpenditure(teamFundId,false);// Tính toán lại các khoản thanh toán sau khi cập nhật
                    if (reCalculateResult.Status == ApiResponseStatusConstant.FailedStatus)
                    {
                        return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                        {
                            Status = ApiResponseStatusConstant.FailedStatus,
                            Message = "Có lỗi xảy ra khi tái tính toán các khoản thanh toán.",
                            Errors = reCalculateResult.Errors
                        };
                    }

                return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Khoản chi cập nhật thành công.",
                    Errors = duplicateWarning.Any() ? duplicateWarning : null
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"Lỗi hệ thống khi cập nhật expenditures: {ex.Message}", ex);
                return new ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Đã xảy ra lỗi hệ thống. Vui lòng thử lại sau.",
                    Errors = new Dictionary<string, string> { { "ServerError", ex.Message } }
                };
            }
        }

        //========================================fuzzy-search==================================
        // Hàm kiểm tra trùng lặp mờ (fuzzy) dựa trên tên và số tiền
        private Dictionary<string, string>? CheckFuzzyDuplicateExpenditures(
            IEnumerable<CreateExpenditureDto> expenditures)
        {
            var duplicates = new Dictionary<string, string>();
            var expenditureList = expenditures.ToList();
            int numberWarning = 0;
            for (int i = 0; i < expenditureList.Count; i++)
            {
                for (int j = i + 1; j < expenditureList.Count; j++)
                {
                    var exp1 = expenditureList[i];
                    var exp2 = expenditureList[j];

                    // Kiểm tra số tiền trùng nhau
                    if (exp1.Amount == exp2.Amount)
                    {
                        // Tính độ tương đồng giữa 2 tên (Levenshtein Distance)
                        double similarity = CalculateStringSimilarity(exp1.Name, exp2.Name);

                        // Ngưỡng similarity 70% (có thể điều chỉnh)
                        if (similarity >= 0.7)
                        {
                            duplicates.Add(
                                key: $"FuzzyWarning_{numberWarning++}",
                                value: $"Cảnh báo: Khoản chi '{exp1.Name}' và '{exp2.Name}' giống nhau {similarity:P2} và cùng số tiền {exp1.Amount}."
                            );
                        }
                    }
                }
            }

            return duplicates.Count != 0 ? duplicates : null;
        }

        // Hàm tính độ tương đồng giữa 2 chuỗi (Levenshtein Distance)
        private double CalculateStringSimilarity(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return 0;

            s1 = s1.ToLower().Trim();
            s2 = s2.ToLower().Trim();

            int maxLen = Math.Max(s1.Length, s2.Length);
            if (maxLen == 0)
                return 1;

            int distance = LevenshteinDistance(s1, s2);
            return 1.0 - (double)distance / maxLen;
        }

        // Thuật toán Levenshtein Distance
        private int LevenshteinDistance(string s, string t)
        {
            int[,] d = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++)
                d[i, 0] = i;
            for (int j = 0; j <= t.Length; j++)
                d[0, j] = j;

            for (int j = 1; j <= t.Length; j++)
            {
                for (int i = 1; i <= s.Length; i++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[s.Length, t.Length];
        }
        //========================================Phần của hiếu==============================================
        public async Task<ApiMessageModelV2<PagedResponseDto<ExpenditureDto>>> GetExpendituresAsync(string? teamFundId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(teamFundId))
            {
                return new ApiMessageModelV2<PagedResponseDto<ExpenditureDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Team Id không được null hoặc bỏ trống"
                };
            }

            var pagedResult = await _teamFundRepository.GetExpendituresAsync(teamFundId, pageNumber, pageSize);

            if (pagedResult == null || !pagedResult.Items.Any())
            {
                return new ApiMessageModelV2<PagedResponseDto<ExpenditureDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = "Không tìm thấy khoản chi nào cho team fund này."
                };
            }

            // Lấy toàn bộ userIds duy nhất từ tất cả UsedByUserId
            var allUserIds = pagedResult.Items
                        .Where(e => !string.IsNullOrWhiteSpace(e.UsedByUserId))
                        .SelectMany(e =>
                        {
                            var match = Regex.Match(e.UsedByUserId!, @"^[01]\((.*?)\)");
                            return match.Success
                                ? match.Groups[1].Value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                : Array.Empty<string>();
                        })
                        .Distinct()
                        .ToList();


            var users = await _playerRepository.GetPlayersByIdsAsync(allUserIds);

            // Ánh xạ từng Expenditure → ExpenditureDto
            var mappedItems = pagedResult.Items.Select(e =>
            {
                var dto = new ExpenditureDto
                {
                    Id = e.ExpenditureId,
                    TeamFundId = e.TeamFundId,
                    Name = e.Name,
                    ByManagerId = e.ByManagerId,
                    Amount = e.Amount,
                    Date = e.Date.ToDateTime(TimeOnly.MinValue),
                    PayoutDate = e.PayoutDate,
                    UsedByUserId = e.UsedByUserId
                };

                if (!string.IsNullOrWhiteSpace(e.UsedByUserId))
                {
                    var match = Regex.Match(e.UsedByUserId, @"^([01])\((.*?)\)");
                    if (match.Success)
                    {
                        var mode = match.Groups[1].Value; // "0" hoặc "1"
                        var ids = match.Groups[2].Value
                            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                        dto.PlayerExpenditures = users
                            .Where(u => ids.Contains(u.UserId))
                            .Select(u => new PlayerExpenditureDto
                            {
                                UserId = u.UserId,
                                Fullname = u.Fullname
                            })
                            .ToList();

                        dto.AllowToEditPlayer = mode == "0"; // chỉ cho edit nếu là 0()
                    }
                }
                else
                {
                    dto.AllowToEditPlayer = true; // nếu null thì vẫn cho edit
                }

                return dto;
            }).ToList();

            return new ApiMessageModelV2<PagedResponseDto<ExpenditureDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Data = new PagedResponseDto<ExpenditureDto>
                {
                    Items = mappedItems,
                    TotalRecords = pagedResult.TotalRecords,
                    CurrentPage = pagedResult.CurrentPage,
                    PageSize = pagedResult.PageSize
                }
            };
        }

        public async Task<ApiMessageModelV2<ExpenditureDto>> DeleteExpendituresAsync(string expenditureId)
        {
            
            // Validate input
            if (string.IsNullOrEmpty(expenditureId))
            {
                return new ApiMessageModelV2<ExpenditureDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "ID của khoản chi không được để trống."
                };
            }

            //lấy đối tượng Expenditure theo ID
            var expenditure = await _teamFundRepository.GetExpenditureByIdAsync(expenditureId);
            if (expenditure == null)
            {
                return new ApiMessageModelV2<ExpenditureDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không tìm thấy khoản chi."
                };
            }

            // Call repository to delete
            var isDeleted = await _teamFundRepository.DeleteExpenditureByIdAsync(expenditureId);
            if (!isDeleted)
            {
                return new ApiMessageModelV2<ExpenditureDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = $"Không tìm thấy khoản chi với ID {expenditureId}."
                };
            }

            //await ReCalculatePaymentWhenChangeExpenditure(expenditure.TeamFundId, true);
            /*-----------------------------------------------------------------------------*/
            _logger.Info($"Bắt đầu tái tính toán Payment từ Expenditure với ");
          
            var teamFundId = expenditure.TeamFundId;
            var teamFund = await _teamFundRepository.GetTeamFundByIdAsync(teamFundId);
            // 1. Xóa tất cả payment và paymentItems
            var isDeleteSuccess = await _teamFundRepository.DeletePaymentsByTeamFundIdAsync(teamFundId);
                if (!isDeleteSuccess)
                {
                    _logger.Error($"Không thể xóa các khoản thanh toán của quỹ {teamFundId}");
                    return new ApiMessageModelV2<ExpenditureDto>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không thể xóa các đơn thanh toán hiện tại.",
                        Errors = new Dictionary<string, string> { { "ErrorDeletePayment", "Không thể xóa một vài đơn thanh toán hoặc dữ liệu liên quan" } }
                    };
                }

                // 3. Lấy danh sách khoản chi theo thời gian
                var expenditures = await _teamFundRepository.GetExpendituresByTeamFundAndDateRangeAsync(teamFundId, teamFund.StartDate, teamFund.EndDate);
                if (expenditures == null || !expenditures.Any())
                 {     
                    return new ApiMessageModelV2<ExpenditureDto>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không tìm thấy khoản chi nào cho quỹ này.",
                        Errors = new Dictionary<string, string> { { "ExpenditureNotFound", "Danh sách khoản chi rỗng" } }
                    };
                }

                // 4. Lấy lịch sử thành viên trong đội để chia tiền
                var userTeamHistory = await _teamFundRepository.GetUserTeamHistoriesByTeamIdAsync(teamFund.TeamId, teamFund.StartDate, teamFund.EndDate);
                if (userTeamHistory == null || !userTeamHistory.Any())
                {
                    return new ApiMessageModelV2<ExpenditureDto>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = "Không tìm thấy lịch sử thành viên của đội.",
                        Errors = new Dictionary<string, string> { { "UserTeamHistoryMissing", "Không có thành viên nào trong khoảng thời gian hoạt động của quỹ" } }
                    };
                }

                var expendituresStartWithOne = await _teamFundRepository.GetExpendituresByTeamFundAndDateRangeStartWithOneAsync(teamFundId, teamFund.StartDate, teamFund.EndDate);
                // 5. Gán danh sách UserId cho mỗi Expenditure cần chia tiền
                await SaveListUsersPaidForExpenditure(expendituresStartWithOne, userTeamHistory, teamFund);

                // 6. Tạo lại các Payment và PaymentItem từ dữ liệu Expenditure mới
                await ExportExpenditureToPaymentInfo(teamFundId);

                _logger.Info($"Hoàn tất tái tính toán Payment cho quỹ {teamFundId}");
            
            /*-----------------------------------------------------------------------------*/
            return new ApiMessageModelV2<ExpenditureDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = "Xóa khoản chi thành công."
            };
        }

        public async Task<ApiMessageModelV2<object>> DeletePlayerInExpendituresAsync(string? expenditureId, string? userId)
        {
            if (string.IsNullOrEmpty(expenditureId) || string.IsNullOrEmpty(userId))
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Thiếu thông tin."
                };
            }

            var expenditure = await _teamFundRepository.GetExpenditureByIdAsync(expenditureId);
            if (expenditure == null || string.IsNullOrEmpty(expenditure.UsedByUserId))
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không tìm thấy khoản chi hoặc chưa có người dùng nào."
                };
            }

            if (expenditure.UsedByUserId!.StartsWith("1("))
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không thể xóa player vì khoản chi này được tạo theo chế độ tự động (1)."
                };
            }

            var match = Regex.Match(expenditure.UsedByUserId, @"0\((.*?)\)");
            if (!match.Success)
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Định dạng dữ liệu không hợp lệ."
                };
            }

            var ids = match.Groups[1].Value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            if (!ids.Remove(userId!))
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không tìm thấy người dùng trong danh sách khoản chi."
                };
            }

            expenditure.UsedByUserId = ids.Any() ? $"0({string.Join(",", ids)})" : null;
            await _teamFundRepository.UpdateExpenditureAsync(expenditure);

            return new ApiMessageModelV2<object>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = "Đã xóa người dùng khỏi khoản chi."
            };
        }

        public async Task<ApiMessageModelV2<object>> AddPlayersToExpenditureAsync(string expenditureId, List<string> userIds)
        {
            if (string.IsNullOrEmpty(expenditureId) || userIds == null || !userIds.Any())
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Thiếu thông tin."
                };
            }

            var expenditure = await _teamFundRepository.GetExpenditureByIdAsync(expenditureId);
            if (expenditure == null)
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không tìm thấy khoản chi."
                };
            }

            if (expenditure.UsedByUserId != null && expenditure.UsedByUserId.StartsWith("1("))
            {
                return new ApiMessageModelV2<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không thể thêm player vào khoản chi này vì được tạo theo chế độ tự động (1)."
                };
            }

            var currentIds = new List<string>();
            var match = Regex.Match(expenditure.UsedByUserId ?? "", @"0\((.*?)\)");
            if (match.Success)
            {
                currentIds = match.Groups[1].Value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            foreach (var id in userIds)
            {
                if (!currentIds.Contains(id))
                {
                    currentIds.Add(id);
                }
            }

            expenditure.UsedByUserId = $"0({string.Join(",", currentIds)})";
            await _teamFundRepository.UpdateExpenditureAsync(expenditure);

            return new ApiMessageModelV2<object>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = "Đã thêm người dùng vào khoản chi."
            };
        }

        public async Task AutoAddExpenditureCourtForTeamFundsAsync()
        {
            await _teamFundRepository.AddExpenditureCourtForTeamFund();
        }

        //======================================================================================
    }
}
