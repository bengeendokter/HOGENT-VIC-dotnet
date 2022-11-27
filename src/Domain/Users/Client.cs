using Ardalis.GuardClauses;
using Domain.Users;
using Domain.VirtualMachines;
using Shared;
using System.Xml.Linq;

namespace Domain.Clients.Users;

public class Client : User
{
    private string phoneNumber = default!;
    public string? PhoneNumber
    {
        get => phoneNumber; 
        set => phoneNumber = Guard.Against.NullOrEmpty(value, nameof(PhoneNumber));
    }

    private string department = default!;
    public string? Department
    {
        get => department;
        set => department = Guard.Against.NullOrEmpty(value, nameof(department));
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
    public string? ClientOrganisation
    {
        get => clientOrganisation;
        set => Guard.Against.NullOrEmpty(value, nameof(ClientOrganisation));
    }

    private string externalType = default!;
    public string? ExternalType
    {
        get => externalType;
        set => externalType = Guard.Against.NullOrWhiteSpace(value, nameof(externalType));
    }

    private string education = default!;
    public string? Education
    {
        get => education;
        set => education = Guard.Against.NullOrWhiteSpace(value, nameof(education));
    }

    // Later veranderen naar VirtualMachineRequest
    // public object[]? VirtualMachineRequests { get; set; }

    private readonly List<VirtualMachine> virtualMachines = new();
    public IReadOnlyCollection<VirtualMachine> VirtualMachines => virtualMachines.AsReadOnly();
    public Client(string phoneNumber, string department, string backupContact, EClientType type, string externalType, string education, string clientOrganisation, string name, string email, string password, ERole role, bool isActive)
        : base(name, email, password, role, isActive)
    {
        PhoneNumber = phoneNumber;
        BackupContact = backupContact;
        Type = type;
        ExternalType = externalType;
        Education = education;
        ClientOrganisation = clientOrganisation;
        Name = name;
        Department = department;
        Password = password;
        Role = role;
        IsActive = isActive;
    }

    public VirtualMachine AddVirtualMachine(VirtualMachine vm)
    {

        virtualMachines.Add(vm);
        return vm;
    }
}
