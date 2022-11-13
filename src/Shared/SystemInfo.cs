using Shared.VirtualMachines;

namespace Shared;

public class SystemInfo
{
    public List<VirtualMachineDto.Index>? VirtualMachines { get; set; }
    public List<Activity>? Activities { get; set; }

    public Stats CalculateUsage()
    {
        return new Stats();
    }
}
