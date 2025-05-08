namespace BasketballAcademyManagementSystemAPI.Common.Constants
{
    public static class TrainingSessionConstant
    {
        public static class Status
        {
            public static int CANCELED = 0;
            public static int ACTIVE = 1;
            public static int PENDING = -1;

            public static string GetStatus(int statusCode)
            {
                switch (statusCode) {
                    case 0:
                        return "CANCELED";
                    case 1:
                        return "ACTIVE";
                    case -1:
                        return "PENDING";
                    default:
                        return "UNKNOWN";
                }

            }
        }

        public static class StatusChangeRequestType 
        {
            public const int CANCEL = 0;
            public const int UPDATE = 1;
        }

        public static class StatusChangeRequestStatus
        {
            public const int PENDING = -1;
            public const int REJECTED = 0;
            public const int APPROVED = 1;

            public static string GetStatus(int statusCode)
            {
                switch (statusCode)
                {
                    case -1:
                        return "PENDING";
                    case 0:
                        return "REJECTED";
                    case 1:
                        return "APPROVED";
                    default:
                        return "UNKNOWN";
                }
            }
        }
    }
}
