using Ardalis.GuardClauses;
using Domain.Users;
using Domain.VirtualMachines;
using System.Xml.Linq;

namespace Domain.Users;

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

    private EClientType clientType = default!;
    public EClientType ClientType
    {
        get => clientType;
        set => clientType = Guard.Against.EnumOutOfRange(value, nameof(ClientType));
    }

    private string clientOrganisation = default!;
    public string? ClientOrganisation
    {
        get => clientOrganisation;
        set => clientOrganisation = Guard.Against.NullOrEmpty(value, nameof(ClientOrganisation));
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
    //public object[]? VirtualMachineRequests { get; set; }

    //private VirtualMachine[] virtualMachines = default!;
    //public VirtualMachine[] VirtualMachines
    //{
    //    get => virtualMachines;
    //    set => virtualMachines = value;
    //}

    public Client(
        string name,
        string surname,
        string email,
        string phoneNumber,
        string backupContact,
        EClientType clientType,
        string clientOrganisation,
        string? education,
        string? externalType,
        bool isActive
    ) : base(name, surname, email, ERole.User, isActive)
    {
        PhoneNumber = phoneNumber;
        BackupContact = backupContact;
        ClientType = clientType;
        ClientOrganisation = clientOrganisation;
        if (clientType == EClientType.Internal)
        {
            Education = education;
        } 
        else if (clientType == EClientType.External)
        {
            ExternalType = externalType;
        }
    }
}
