namespace Shared.Activities;

public abstract class ActivityRequest
{
    public class Index
    {
        public string? SortingParameter { get; set; } = "date";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
