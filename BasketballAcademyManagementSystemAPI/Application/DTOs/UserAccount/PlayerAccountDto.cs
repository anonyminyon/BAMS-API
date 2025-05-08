namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount
{
    public class PlayerAccountDto
    {
        public string UserId { get; set; } = null!;

        public string? ParentId { get; set; }

        public string? ParentName { get; set; }

        public string? TeamId { get; set; }

        public string? TeamName { get; set; }

        public string? RelationshipWithParent { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public string Position { get; set; } = null!;

        public int ShirtNumber { get; set; }

        public string JoinDate { get; set; }
    }
}
