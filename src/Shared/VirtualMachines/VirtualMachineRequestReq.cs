namespace Shared.VirtualMachines
{
    public abstract class VirtualMachineRequestReq
    {
        public class Index
        {
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 25;
            public string? Status { get; set; }
            public string? SortBy { get; set; }
        }
    }
}

