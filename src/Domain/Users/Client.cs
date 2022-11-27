using Ardalis.GuardClauses;
using Domain.Users;
using Domain.VirtualMachines;
using Shared;

namespace Domain.Clients;

public class Client : User
{
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

    public string? ExternalType { get; set; }
    public string? Education { get; set; }

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
        string email,
        string phoneNumber,
        string backupContact,
        EClientType clientType,
        string clientOrganisation,
        string? education,
        string? externalType
    ) : base(name, email, ERole.User)
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
