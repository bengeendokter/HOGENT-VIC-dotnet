using Domain.Common;

namespace Domain.Clients;

public class Client : Entity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BackupContact { get; set; }

    // Later veranderen naar User klasse
    public object? User { get; set; }

    // Later veranderen naar VirtualMachineRequest
    public object[]? VirtualMachineRequests { get; set; }

}
