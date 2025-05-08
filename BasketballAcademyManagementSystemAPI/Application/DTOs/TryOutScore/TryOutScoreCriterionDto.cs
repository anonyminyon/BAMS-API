namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class TryOutScoreCriterionDto
    {
        public int ScoreCriteriaId { get; set; }
        public string CriteriaName { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public bool Gender { get; set; }
        public List<TryOutScoreLevelDto> ScoreLevels { get; set; } = new();
    }
}
