using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Users;
using Domain.VirtualMachines;
using System.Xml.Linq;

namespace Domain.Users;

public class Client : Entity
{

    private string name = default!;
    public string Name
    {
        get => name;
        set => name = Guard.Against.NullOrEmpty(value, nameof(Name));
    }

    private string surname = default!;
    public string Surname
    {
        get => surname;
        set => surname = Guard.Against.NullOrEmpty(value, nameof(surname));
    }

    private string email = default!;
    public string Email
    {
        get => email;
        set => email = Guard.Against.NullOrEmpty(value, nameof(Email));
    }

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
        //set => externalType = Guard.Against.NullOrWhiteSpace(value, nameof(externalType));
        set => externalType = value;
    }

    private string education = default!;
    public string? Education
    {
        get => education;
        //set => education = Guard.Against.NullOrWhiteSpace(value, nameof(education));
        set => education = value;
    }

    private readonly List<VirtualMachine> virtualMachines = new();
    public IReadOnlyCollection<VirtualMachine> VirtualMachines => virtualMachines.AsReadOnly();
    public void AddVM(VirtualMachine vm)
    {
        if (!IsEnabled)
            throw new ApplicationException($"{nameof(Client)} is not active, could not add virtual machine.");

        virtualMachines.Add(vm); 
    }

    public void RemoveVM(VirtualMachine vm)
    {
        if (!IsEnabled)
            throw new ApplicationException($"{nameof(Client)} is not active, could not remove virtual machine.");

        virtualMachines.Remove(vm);
    }

    private readonly List<VirtualMachineRequest> requests = new();
    public IReadOnlyCollection<VirtualMachineRequest> Requests => requests.AsReadOnly();
    public void AddRequest(VirtualMachineRequest request)
    {
        requests.Add(request);
    }

    public void RemoveRequest(VirtualMachineRequest request)
    {
        requests.Remove(request);
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
        string? externalType
    )
    {
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
        BackupContact = backupContact;
        ClientType = clientType;
        ClientOrganisation = clientOrganisation;
        if (clientType == EClientType.Internal)
        {
            Education = education;
            ClientOrganisation = "HOGENT";
        } 
        else if (clientType == EClientType.External)
        {
            ExternalType = externalType;
        }
    }
}
