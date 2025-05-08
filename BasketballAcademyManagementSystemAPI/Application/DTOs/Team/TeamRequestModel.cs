
namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Team
{
	public class TeamRequestModel
	{

		public string TeamName { get; set; }
		public int? Status { get; set; }

		public static implicit operator TeamRequestModel?(TeamDetailsDto? v)
		{
			throw new NotImplementedException();
		}
	}
}
