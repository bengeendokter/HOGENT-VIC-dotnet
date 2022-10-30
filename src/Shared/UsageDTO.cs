using System;

namespace Shared;

public class UsageDTO
{
    public class Index
    {
        public EUsageType UsageType { get; set; }
        public EUsageTypeUnit Eenheid { get; set; }

        public decimal Total { get; set; }

        public decimal Gebruikt { get; set; }
    }
}
