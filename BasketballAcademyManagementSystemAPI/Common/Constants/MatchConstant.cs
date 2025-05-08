namespace BasketballAcademyManagementSystemAPI.Common.Constants
{
    public static class MatchConstant
    {
        public static class Status
        {
            public const int UPCOMING = 0;
            public const int ONGOING = 1;
            public const int FINISHED = 2;
            public const int CANCELED = 3;

            public static string GetStatusName(int status)
            {
                return status switch
                {
                    UPCOMING => "Sắp diễn ra",
                    ONGOING => "Đang diễn ra",
                    FINISHED => "Đã kết thúc",
                    CANCELED => "Đã hủy",
                    _ => "Không xác định"
                };
            }
        }

        public static class Setting
        {
            public const string MatchSettingsSection = "MatchSettings";
            public const string MinimumAdvanceScheduling = "MinimumAdvanceScheduling";
            public const string MinimumHourDurationOfAMatch = "MinimumHourDurationOfAMatch";
            public const string MaxStartingPlayers = "MaxStartingPlayers";
            public const string MaxRegistraionPlayers = "MaxRegistraionPlayers";
        }

        public static class AdditionalErrorField
        {
            public const string BothTeamId = "BothTeamId";
        }

        public static class ArticleType
        {
            public const string VIDEO = "VIDEO";
            public const string DOCUMENT = "DOCUMENT";
            public const string IMAGE = "IMAGE";

            private static readonly HashSet<string> ValidTypes = new()
            {
                VIDEO, DOCUMENT, IMAGE
            };

            public static bool IsValid(string input)
            {
                return ValidTypes.Contains(input);
            }
        }

        public static class ArticleSaveLocaltion
        {
            public static string VideoPath(int matchId)
            {
                return $"match-articles/{matchId}/videos";
            }

            public static string DocumentPath(int matchId)
            {
                return $"match-articles/{matchId}/documents";
            }
            public static string ImagePath(int matchId)
            {
                return $"match-articles/{matchId}/images";
            }
        }
    }
}
