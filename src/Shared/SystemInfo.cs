using Shared.VirtualMachines;
using Shared.Activities;

namespace Shared;

public class SystemInfo
{
    public List<VirtualMachineDto.Index>? VirtualMachines { get; set; }
    public List<ActivityDto.Index>? Activities { get; set; }

    public Stats CalculateUsage()
    {
        return new Stats();
    }
}
