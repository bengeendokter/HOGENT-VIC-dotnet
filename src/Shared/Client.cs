using Shared.VirtualMachines;

namespace Shared;

public class Client
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BackupContact { get; set; }
    public User? User { get; set; }
    public VirtualMachineRequest[]? VirtualMachineRequests { get; set; }
}
