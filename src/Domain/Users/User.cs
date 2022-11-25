using Ardalis.GuardClauses;
using Domain.Common;
using Domain.VirtualMachines;
using Shared;

namespace Domain.Users;

public class User : Entity
{
    private string name = default!;
    public string Name
    {
        get => name;
        set => name = Guard.Against.NullOrWhiteSpace(value, nameof(Name));
    }

    private string department = default!;
    public string Department
    {
        get => department;
        set => department = Guard.Against.NullOrWhiteSpace(value, nameof(Department));
    }
    private string password = default!;
    public string Password
    {
        get => password;
        set => password = Guard.Against.NullOrWhiteSpace(value, nameof(Password));
    }
    private ERole role = default!;
    public ERole Role
    {
        get => role;
        set => role = Guard.Against.EnumOutOfRange(value, nameof(Role));
    }
    private List<VirtualMachine>? virtualMachines = default!;
    public List<VirtualMachine> VirtualMachines
    {
        get => virtualMachines;
        set => virtualMachines = Guard.Against.Null(value, nameof(virtualMachines));
    }
    private bool isActive = default!;
    public bool IsActive
    {
        get => isActive;
        set => isActive = Guard.Against.Null(value, nameof(isActive));
    }

    private User() { }

    public User(string name, string department, string password, ERole role, List<VirtualMachine> virtualMachines, bool isActive)
    {
        Name = name;
        Department = department;
        Password = password;
        Role = role;
        VirtualMachines = virtualMachines;
        IsActive = isActive;
    }
}
