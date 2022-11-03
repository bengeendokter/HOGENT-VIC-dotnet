using Shared.VirtualMachines;

namespace Shared;

public class Activity
{
    public VirtualMachine? VirtualMachine { get; set; }
    public DateTime Date { get; set; }
    public EActivity Type { get; set; }
}
