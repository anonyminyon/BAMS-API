using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.VietQr;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
	public interface IVietQRService
	{
		Task<ApiMessageModelV2<object>> GenerateQrAsync(string paymentId);

	}
}
