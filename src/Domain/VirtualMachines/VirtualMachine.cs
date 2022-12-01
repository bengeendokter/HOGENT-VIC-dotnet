using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.VirtualMachines;

public class VirtualMachine : Entity
{
    // TODO: relation with client domain class
    /*private string client = default!;
    public string Client
    {
        get => client;
        set => client = Guard.Against.Null(value, nameof(Client));
    }*/

    private string name = default!;
    public string Name
    {
        get => name;
        set => name = Guard.Against.NullOrEmpty(value, nameof(Name));
    }

    private string hostName = default!;
    public string HostName
    {
        get => hostName;
        set => hostName = Guard.Against.NullOrEmpty(value, nameof(HostName));
    }

    private DateTime startDate = default!;
    public DateTime StartDate
    {
        get => startDate;
        set =>
            startDate = Guard.Against.AgainstExpression(
                d => d > DateTime.Now,
                value,
                "StartDate must be later than now"
            );
    }

    private DateTime endDate = default!;
    public DateTime EndDate
    {
        get => endDate;
        set =>
            endDate = Guard.Against.AgainstExpression(
                d => d > startDate,
                value,
                "EndDate must be greater than StartDate"
            );
    }

    private string fqdn = default!;
    public string FQDN
    {
        get => fqdn;
        set => fqdn = Guard.Against.NullOrEmpty(value, nameof(FQDN));
    }

    private string poorten = default!;
    public string Poorten
    {
        get => poorten;
        set => poorten = Guard.Against.NullOrEmpty(value, nameof(Poorten));
    }

    // TODO: relation with template domain class
    private ETemplate template = default!;
    public ETemplate Template
    {
        get => template;
        set => template = Guard.Against.EnumOutOfRange(value, nameof(Template));
    }

    private string host = default!;
    public string Host
    {
        get => host;
        set => host = Guard.Against.NullOrEmpty(value, nameof(Host));
    }

    private int cpu = default!;
    public int CPU
    {
        get => cpu;
        set => cpu = Guard.Against.NegativeOrZero(value, nameof(CPU));
    }

    private int ram = default!;
    public int RAM
    {
        get => ram;
        set => ram = Guard.Against.NegativeOrZero(value, nameof(RAM));
    }

    private int storage = default!;
    public int Storage
    {
        get => storage;
        set => storage = Guard.Against.NegativeOrZero(value, nameof(Storage));
    }

    private EMode mode = default!;
    public EMode Mode
    {
        get => mode;
        set => mode = Guard.Against.EnumOutOfRange(value, nameof(Mode));
    }

    private EBackupFrequency backupFrequency = default!;
    public EBackupFrequency BackupFrequency
    {
        get => backupFrequency;
        set => backupFrequency = Guard.Against.EnumOutOfRange(value, nameof(BackupFrequency));
    }

    private EDay availability = default!;
    public EDay Availability
    {
        get => availability;
        set => availability = (EDay)Guard.Against.OutOfRange((int)value, nameof(EDay), 1, 127);
    }

    public bool IsHighlyAvailable { get; set; }
    public bool IsActive { get; set; }

    /// <summary>
    /// Database Constructor
    /// </summary>
    private VirtualMachine() { }

    public VirtualMachine(
        // object client,
        string name,
        string hostName,
        DateTime startDate,
        DateTime endDate,
        string fqdn,
        string poorten,
        ETemplate template,
        string host,
        int cpu,
        int ram,
        int storage,
        EMode mode,
        EBackupFrequency backupFrequency,
        EDay availability,
        bool isHighlyAvailable,
        bool isActive
    )
    {
        // Client = client;
        Name = name;
        HostName = hostName;
        StartDate = startDate;
        EndDate = endDate;
        FQDN = fqdn;
        Poorten = poorten;
        Template = template;
        Host = host;
        CPU = cpu;
        RAM = ram;
        Storage = storage;
        Mode = mode;
        BackupFrequency = backupFrequency;
        Availability = availability;
        IsHighlyAvailable = isHighlyAvailable;
        IsActive = isActive;
    }
}
