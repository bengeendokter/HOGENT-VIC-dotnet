namespace Shared.VirtualMachines
{
    public abstract class VirtualMachineReq
    {
        public class Index
        {
            public string? Searchterm { get; set; }
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 25;
            public string? SortBy { get; set; }
            public string? Status { get; set; }
            public string? HoogBeschikbaar { get; set; }

        }
    }
}
