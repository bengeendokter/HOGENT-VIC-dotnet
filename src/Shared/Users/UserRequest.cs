namespace Shared.Clients;

public abstract class UserRequest
{
    public class Index
    {
        public string? Searchterm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string? Role { get; set; }
    }
}
