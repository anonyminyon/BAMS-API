namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Team
{
	public class RemovePlayerRequest
	{
		public string TeamId { get; set; }
		public List<string> PlayerIds { get; set; } = new List<string>();

	}
}
