namespace Shared;

public class UsageDTO
{
    public class Index
    {
        public EUsage UsageType { get; set; }
        public EUsageUnit Unit { get; set; }
        public decimal Total { get; set; }
        public decimal Used { get; set; }
    }
}
