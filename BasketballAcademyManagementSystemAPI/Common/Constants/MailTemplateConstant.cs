namespace BasketballAcademyManagementSystemAPI.Common.Constants
{
    public class MailTemplateConstant
    {
        public static string ChangedPasswordEmailId = "CHANGE_PASSWORD_SUCCESS";
        public static string ForgotPasswordTokenEmailId = "FORGOT_PASSWORD_TOKEN";
        public static string ApproveManagerRegistration = "APPROVED_MANAGER_REGISTRATION";
        public static string RejectManagerRegistration = "REJECT_MANAGER_REGISTRATION";
        public static string ManagerAssignToTeamSuccess = "MANAGER_ASSIGN_TO_TEAM_SUCCESS";
        public static string CoachAssignToTeamSuccess = "COACH_ASSIGN_TO_TEAM_SUCCESS";
        public static string PlayerAssignToTeamSuccess = "PLAYER_ASSIGN_TO_TEAM_SUCCESS";
        public static string SendFormRegistrationSuccess = "SEND_FORM_REGISTRATION_SUCCESS";
        public static string ResetPasswordSuccess = "RESET_PASSWORD_SUCCESS";
		public static string VerifyEmailRegistration = "VERIFY_EMAIL_REGISTRATION";
		public static string RejectPlayerRegistration = "REJECT_PLAYER_REGISTRATION";
		public static string CallToTryOut = "CALL_TRY_OUT";
		public static string ApprovedPlayerEmailRegistration = "APPROVED_PLAYER_EMAIL_REGISTRATION";
		public static string ApprovedParentEmailRegistration = "APPROVED_PARENT_EMAIL_REGISTRATION";
        public static string DisableAccount = "DISABLE_ACCOUNT";
        public static string EnableAccount = "ENABLE_ACCOUNT";
        public static string CoachRegistrationSuccess = "COACH_REGISTRATION_SUCCESS";
		public static string CreateAdditionalTrainingSession = "CREATE_ADDITIONAL_TRAINING_SESSION";
		public static string RejectPendingTrainingSession = "REJECT_PENDING_TRAINING_SESSION";
		public static string UpdateTrainingSession = "UPDATE_TRAINING_SESSION";
		public static string CancelTrainingSession = "CANCEL_TRAINING_SESSION";
        public static string AdditionalTrainingSessionPending = "ADDITIONAL_TRAINING_SESSION_PENDING";
        public static string RejectCancelTrainingSessionRequest = "REJECT_CANCEL_TSRQ";
        public static string RejectUpdateTrainingSessionRequest = "REJECT_UPDATE_TSRQ";
		public static string RequireParentPayment = "REQUIRE_PARENT_PAYMENT";
		public static string RequirePlayerPayment = "REQUIRE_PLAYER_PAYMENT";
		public static string ReminderPayment = "REMINDER_PAYMENT";
		public static string ReportAttendanceToParent = "REPORT_ATTENDANCE_TO_PARENT";
		public static string CorrectionAttendanceToParent = "CORRECTION_ATTENDANCE_TO_PARENT";
		public static string RejectTeamFund = "REJECT_TEAMFUND";
        
		public static string URL_TO_BAMS = "https://localhost:5000/api/auth/login";
	}
}
