namespace Shared;

public class VirtualMachineRequest
{
    public VirtualMachine? VirtualMachine { get; set; }
    public ERequestStatus Status { get; set; }
    public DateTime Date { get; set; }
}
