using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Activities;

public class Activity : Entity
{
    private EActivity type = default!;
    public EActivity Type
    {
        get => type;
        set => type = Guard.Against.EnumOutOfRange(value, nameof(Type));
    }

    private string vmName = default!;
    public string VMName
    {
        get => vmName;
        set => vmName = Guard.Against.NullOrEmpty(value, nameof(VMName));
    }

    private string clientName = default!;
    public string ClientName
    {
        get => clientName;
        set => clientName = Guard.Against.NullOrEmpty(value, nameof(ClientName));
    }

    private int cpu = default!;
    public int CPU
    {
        get => cpu;
        set => cpu = Guard.Against.Null(value, nameof(CPU));
    }

    private int ram = default!;
    public int RAM
    {
        get => ram;
        set => ram = Guard.Against.Null(value, nameof(RAM));
    }

    private int storage = default!;
    public int Storage
    {
        get => storage;
        set => storage = Guard.Against.Null(value, nameof(Storage));
    }

    /// <summary>
    /// Database Constructor
    /// </summary>
    public Activity() { }

    public Activity(EActivity type, string vmName, string clientName, int cpu, int ram, int storage)
    {
        Type = type;
        VMName = vmName;
        ClientName = clientName;
        CPU = cpu;
        RAM = ram;
        Storage = storage;
    }
}
