namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class BulkPlayerScoreInputDto
    {
        public List<PlayerScoreInputDto> Players { get; set; } = new();
    }
}
