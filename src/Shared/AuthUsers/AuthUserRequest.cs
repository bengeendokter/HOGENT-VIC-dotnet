namespace Shared.AuthUsers;

public class AuthUserRequest
{
    public class Index
    {
        public string? Searchterm { get; set; }
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 25;
        public string? Role { get; set; }
    }
    public class General
    {
        public bool Blocked { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ScreenName { get; set; }
    }
    public class Roles
    {
        public bool IsUser { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsModerator { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
