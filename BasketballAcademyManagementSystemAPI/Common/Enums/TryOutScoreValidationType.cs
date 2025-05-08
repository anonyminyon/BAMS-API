namespace BasketballAcademyManagementSystemAPI.Common.Enums
{
    public enum TryOutScoreValidationType
    {
        ThreeLevelsGrade,   // Chỉ nhận các chuỗi "T", "K", "TB"
        Range1To5, // Chỉ nhận số từ 1 - 5
        PhysicalFitnessValue // Nhận các con số mà cầu thủ thực hiện được
    }
}
