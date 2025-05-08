using BasketballAcademyManagementSystemAPI.Common.Enums;
using Microsoft.EntityFrameworkCore.Query;

namespace BasketballAcademyManagementSystemAPI.Common.Constants
{
    public static class TryOutScoreConstant
    {
        public static readonly Dictionary<string, TryOutScoreValidationType> ValidationRules = new()
        {
            { "Dribble", TryOutScoreValidationType.ThreeLevelsGrade },
            { "Passing", TryOutScoreValidationType.ThreeLevelsGrade },
            { "Shooting", TryOutScoreValidationType.ThreeLevelsGrade },
            { "Finishing", TryOutScoreValidationType.ThreeLevelsGrade },
            { "Attitude", TryOutScoreValidationType.Range1To5 },
            { "Leadership", TryOutScoreValidationType.Range1To5 },
            { "Skills", TryOutScoreValidationType.Range1To5 },
            { "ScrimmagePhysicalFitness", TryOutScoreValidationType.Range1To5 },
            { "BasketballIQ", TryOutScoreValidationType.Range1To5 },
            { "HexagonTest", TryOutScoreValidationType.PhysicalFitnessValue },
            { "StandingBroadJump", TryOutScoreValidationType.PhysicalFitnessValue },
            { "StandingVerticalJump", TryOutScoreValidationType.PhysicalFitnessValue },
            { "RunningVerticalJump", TryOutScoreValidationType.PhysicalFitnessValue },
            { "AgilityTest", TryOutScoreValidationType.PhysicalFitnessValue },
            { "Sprint", TryOutScoreValidationType.PhysicalFitnessValue },
            { "PushUp", TryOutScoreValidationType.PhysicalFitnessValue },
            { "StandardPlank", TryOutScoreValidationType.PhysicalFitnessValue },
            { "RightSidePlank", TryOutScoreValidationType.PhysicalFitnessValue },
            { "LeftSidePlank", TryOutScoreValidationType.PhysicalFitnessValue }
        };


        public static string BasketballSkill = "BasketballSkill";
        public static string PhysicalFitness = "PhysicalFitness";

        public static string TryOutScoreReportFileName = "BÁO CÁO KẾT QUẢ ĐÁNH GIÁ NĂNG LỰC YHBT";
        public static string TryOutScoreReportSheetName = "Báo cáo";
        public static string TryOutScoreRawScoreSheetName = "Điểm gốc";

        public static class TryOutReportPlayerInfColIndex
        {
            public const int CandidateColumnIndex = 1; // SBD
            public const int PlayerRegistrationIdColumnIndex = 2; // Mã đơn
            public const int FullNameColumnIndex = 3; // Họ và tên
            public const int GenderColumnIndex = 4; // Giới tính
            public const int BirthDateColumnIndex = 5; // Ngày sinh
            public const int AverageScoreColumnIndex = 6; // Điểm trung bình
            public const int BasketballSkillAverageScoreColumnIndex = 7; // Điểm kỹ năng
            public const int PhysicalFitnessAverageScoreColumnIndex = 8; // Điểm thể lực
        }

        public static class TryOutReportPlayerInfColHeaders
        {
            public const string CandidateHeader = "SBD";
            public const string OrderCodeHeader = "Mã đơn";
            public const string FullNameHeader = "Họ và tên";
            public const string GenderHeader = "Giới tính";
            public const string BirthDateHeader = "Ngày sinh";
            public const string AverageScoreHeader = "Điểm trung bình";
            public const string SkillScoreHeader = "Điểm kỹ năng";
            public const string PhysicalScoreHeader = "Điểm thể lực";
        }
    }
}
