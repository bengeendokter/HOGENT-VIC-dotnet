namespace Shared.AuthUsers;

public class AuthUserResponse
{
    public class Create
    {
        public class Role
        {
            public IEnumerable<AuthUserDto.Detail.UserRole> rollen { get; set; }
        }
    }
}
