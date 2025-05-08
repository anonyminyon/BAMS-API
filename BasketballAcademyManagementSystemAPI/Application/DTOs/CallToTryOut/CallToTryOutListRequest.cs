namespace BasketballAcademyManagementSystemAPI.Application.DTOs.CallToTryOut
{
	public class CallToTryOutListRequest
	{
		public List<int> PlayerRegistIds { get; set; }
		public string Location { get; set; }
		public DateTime TryOutDateTime { get; set; }
	}
}
