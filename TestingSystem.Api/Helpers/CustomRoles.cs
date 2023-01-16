namespace TestingSystem.Api.Helpers
{
    public class CustomRoles
    {
        private const string ADMIN = "Admin";
        private const string USER = "User";
        private const string TEACHER = "Teacher";

        public const string USER_ROLE = USER + "," + ADMIN_ROLE;
        public const string ADMIN_ROLE = ADMIN;
        public const string ALL_ROLES = ADMIN + "," + TEACHER + "," + USER;
        public const string TEACHER_ROLE = TEACHER + "," + ADMIN;

    }
}
