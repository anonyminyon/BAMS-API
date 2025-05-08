using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.SePay;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ITeamFundService
	{
		Task AutoCreateTeamFundsAsync(bool skipDateCheck = false);
		Task<ApiMessageModelV2<List<TeamFundListDto>>> GetTeamFundsAsync(TeamFundFilterDto filter);
		Task<ApiMessageModelV2<object>> UpdateStatusPaymentAutoSepay(SePayWebhookDto dataPaymentResponse);
		Task<ApiMessageModelV2<string>> ApproveTeamFundAsync(string teamFundId);
		Task<ApiMessageModelV2<List<PlayerExpenditureResponseDto>>> GetPlayerUserIdsByTeamAndDateAsync(string teamId, DateTime targetDate);
		//phần của hiếu
		Task<ApiMessageModelV2<IEnumerable<ExpenditureDto>>> AddExpendituresAsync(IEnumerable<CreateExpenditureHasListUserIdDto> expenditures, string teamFundId);
        Task<ApiMessageModelV2<IEnumerable<UpdateExpenditureDto>>> UpdateExpendituresAsync(IEnumerable<UpdateExpenditureHasListUserIdDto> expenditures,string teamFundId);

        Task<ApiMessageModelV2<ExpenditureDto>> DeleteExpendituresAsync(string expenditureId);
        Task<ApiMessageModelV2<object>> DeletePlayerInExpendituresAsync(string? expenditureId, string? userId);
        Task<ApiMessageModelV2<object>> AddPlayersToExpenditureAsync(string expenditureId, List<string> userIds);
        Task<ApiMessageModelV2<PagedResponseDto<ExpenditureDto>>> GetExpendituresAsync(string? teamFundId, int pageNumber, int pageSize);
		Task AutoAddExpenditureCourtForTeamFundsAsync();
		//==========
		Task<ApiMessageModelV2<object>> GetManagerBankInfor(string paymentId);
		Task<ApiMessageModelV2<object>> UpdateStatusPayment(UpdatePaymentStatusRequest request);

		Task<ApiMessageModelV2<object>> RejectTeamFund(string teamFundId, string reasonReject);
	}

	
}

