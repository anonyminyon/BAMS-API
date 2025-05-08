using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Team
{
	public class RemovePlayerInTeamRequest
	{
		public string TeamId { get; set; }
		
		public List<string> PlayerIds { get; set; }
	}
}
