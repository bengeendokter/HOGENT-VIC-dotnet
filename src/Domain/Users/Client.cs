using Ardalis.GuardClauses;
using Domain.Users;
using Domain.VirtualMachines;
using Shared;

namespace Domain.Clients;

public class Client : User
{
    public new ERole Role = ERole.User;

    private string phoneNumber = default!;
    public string? PhoneNumber
    {
        get => phoneNumber; 
        set => phoneNumber = Guard.Against.NullOrEmpty(value, nameof(PhoneNumber));
    }

    private string backupContact = default!;
    public string? BackupContact
    {
        get => backupContact;
        set => backupContact = Guard.Against.NullOrEmpty(value, nameof(BackupContact));
    }

    private EClientType type = default!;
    public EClientType Type
    {
        get => type;
        set => Guard.Against.EnumOutOfRange(value, nameof(Type));
    }

    private string clientOrganisation = default!;
    public string ClientOrganisation
    {
        get => clientOrganisation;
        set => Guard.Against.NullOrEmpty(value, nameof(ClientOrganisation));
    }

    public string? ExternalType { get; set; }
    public string? Education { get; set; }

    // Later veranderen naar VirtualMachineRequest
    public object[]? VirtualMachineRequests { get; set; }

    private VirtualMachine[] virtualMachines = default!;
    public VirtualMachine[] VirtualMachines
    {
        get => virtualMachines;
        set => virtualMachines = value;
    }
}
