using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Enums;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
    public class TryOutScoreValidationHelper
    {
        public static bool IsValidScore(string measurementScaleCode, string score)
        {
            if (!TryOutScoreConstant.ValidationRules.TryGetValue(measurementScaleCode, out var validationType))
            {
                return false; // Nếu không tìm thấy mã, coi như không hợp lệ
            }

            return validationType switch
            {
                TryOutScoreValidationType.ThreeLevelsGrade => IsValidThreeLevelsGrade(score),
                TryOutScoreValidationType.PhysicalFitnessValue => IsValidFitnessValue(score),
                TryOutScoreValidationType.Range1To5 => IsValidRange1To5(score),
                _ => false
            };
        }

        private static bool IsValidThreeLevelsGrade(string score)
        {
            return score is "T" or "K" or "TB";
        }

        private static bool IsValidFitnessValue(string score)
        {
            return double.TryParse(score, out var value) && value > 0;
        }

        private static bool IsValidRange1To5(string score)
        {
            return int.TryParse(score, out var value) && value >= 1 && value <= 5;
        }
    }
}
