using Shared.VirtualMachines;

namespace Shared;

public class User
{
    public string? Name { get; set; }
    public string? Department { get; set; }
    public string? Password { get; set; }
    public ERole Role { get; set; }
    public List<VirtualMachineDto.Index>? VirtualMachines { get; set; }
    public bool IsActive { get; set; }
}