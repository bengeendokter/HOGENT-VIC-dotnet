using Domain.Common;
using Domain.VirtualMachines;

namespace Domain.Activities;

public class Activity : Entity
{
    public VirtualMachine? VirtualMachine { get; set; }
    public EActivity Type { get; set; }
}
