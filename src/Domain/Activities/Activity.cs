using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Users;
using Domain.VirtualMachines;

namespace Domain.Activities;

public class Activity : Entity
{
    public VirtualMachine VirtualMachine { get; } = default!;

    private EActivity type = default!;
    public EActivity Type
    {
        get => type;
        set => type = Guard.Against.EnumOutOfRange(value, nameof(Type));
    }

    /// <summary>
    /// Database Constructor
    /// </summary>
    public Activity() { }

    public Activity(VirtualMachine virtualMachine, EActivity type)
    {
        VirtualMachine = Guard.Against.Null(virtualMachine, nameof(VirtualMachine));
        Type = type;
    }
}
