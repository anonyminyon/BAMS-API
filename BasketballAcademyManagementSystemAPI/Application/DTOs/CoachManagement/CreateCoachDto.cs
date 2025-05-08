namespace BasketballAcademyManagementSystemAPI.Application.DTOs.CoachManagement
{
    public class CreateCoachDto
    {
        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? ProfileImage { get; set; }

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        //below is attribute from coach table
        public DateTime ContractStartDate { get; set; } 

        public DateTime ContractEndDate { get; set; } 
    }
}
