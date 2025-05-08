namespace BasketballAcademyManagementSystemAPI.Common.Constants
{
    public static class FaceRecognitionConstant
    {
        public static class Setting
        {
            public const string FaceRecognitionSettingSection = "FaceRecognitionSettings";
            public const string MaxImageSize = "MaxImageSize";
            public static List<string> ValidExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".heic" };
        }

        public const string UnKnownFace = "Không xác định";
    }
}
