using Shared.Clients;

namespace Shared.Users;

public abstract class UserResult
{
    public class Index
    {
        public IEnumerable<UserDto.Index>? Users { get; set; }
        public int TotalAmount { get; set; }
    }
}
