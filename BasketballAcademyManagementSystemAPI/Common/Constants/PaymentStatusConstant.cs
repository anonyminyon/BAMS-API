namespace BasketballAcademyManagementSystemAPI.Common.Constants
{
    public static class PaymentStatusConstant
    {
        public static int NOT_PAID = 0;           // Chưa trả
        public static int PAID_CONFIRMED = 1;     // Đã trả và được xác nhận
        public static int OVERDUE = 2;            // Quá hạn
        public static int PAID_UNCONFIRMED = 3;   // Đã trả nhưng chưa được xác nhận
        public static int WAIT_TO_COMPLAIN = 4;   // Đang chờ player khiếu nại
    }
}