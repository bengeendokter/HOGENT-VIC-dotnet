namespace Shared.Activities;

public static class ActivityDto
{
    public class Index
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public EActivity Type { get; set; }
        public string? VMName { get; set; }
        public string? ClientName { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
    }
}
