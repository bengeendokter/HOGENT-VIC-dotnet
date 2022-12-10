namespace Shared.VirtualMachines
{
    public abstract class VirtualMachineReq
    {
        public class Index
        {
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 25;
        }
    }
}
