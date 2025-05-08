namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class UserTeamHistoryMessage
    {
        public static class Error
        {
            public const string UserAlreadyInTeam = "You are already a member of this team.";
            public const string AssignError = "An error occurred while assigning the coach to the team.";
        }
        public static class Success
        {

        }
        public static class Key
        {

        }
    }

}
