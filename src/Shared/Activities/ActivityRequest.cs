namespace Shared.Activities;

public abstract class ActivityRequest
{
    public class Index
    {
        public string? SortBy { get; set; } = "date";
        public string? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
