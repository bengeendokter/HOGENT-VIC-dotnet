using Shared.Activities;
using Shared.VirtualMachines;

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
