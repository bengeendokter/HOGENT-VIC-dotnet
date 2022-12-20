namespace Shared.AuthUsers;

public class AuthUserRequest
{
    public class Roles
    {
        public bool IsUser { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsModerator { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
