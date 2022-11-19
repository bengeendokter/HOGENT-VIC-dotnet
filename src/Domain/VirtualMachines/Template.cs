using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.VirtualMachines;

public class Template : Entity
{
    private ETemplate type = default!;
    public ETemplate Type
    {
        get => type;
        set => type = Guard.Against.EnumOutOfRange(value, nameof(Type));
    }

    private EMode mode = default!;
    public EMode Mode
    {
        get => mode;
        set => mode = Guard.Against.EnumOutOfRange(value, nameof(Mode));
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

    /// <summary>
    /// Database Constructor
    /// </summary>
    private Template() { }

    // TODO: public constructor
}
