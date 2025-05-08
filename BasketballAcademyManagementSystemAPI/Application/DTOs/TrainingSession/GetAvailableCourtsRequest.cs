using System.ComponentModel.DataAnnotations;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class GetAvailableCourtsRequest
    {
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        [EnumDataType(typeof(DayOfWeek), ErrorMessage = "Ngày trong tuần đã chọn không hợp lệ.")]
        public DayOfWeek? DayOfWeek { get; set; }
    }
}
