namespace Shared;

public class UsageDto
{
    public class Index
    {
        public EUsage UsageType { get; set; }
        public EUsageUnit Unit { get; set; }
        public decimal Total { get; set; }
        public decimal Used { get; set; }
    }
}
