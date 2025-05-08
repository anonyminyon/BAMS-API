using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BasketballAcademyManagementSystemAPI.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // Chuyển đổi enum thành string khi gửi/nhận JSON
    public enum CourtUsagePurpose
    {
        [Display(Name = "Thi đấu")]
        Compete = 1,

        [Display(Name = "Tập luyện")]
        Training = 2,

        [Display(Name = "Cả thi đấu và tập luyện")]
        CompeteAndTraining = 3
    }
}