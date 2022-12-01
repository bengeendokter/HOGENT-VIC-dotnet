using Ardalis.GuardClauses;
using Domain.Common;
using Domain.VirtualMachines;

namespace Domain.Users;

public class User : Entity
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

    private ERole role = default!;
    public ERole Role
    {
        get => role;
        set => role = Guard.Against.EnumOutOfRange(value, nameof(Role));
    }

    private bool isActive = default!;
    public bool IsActive
    {
        get => isActive;
        set => isActive = Guard.Against.Null(value, nameof(isActive));
    }

    public User(string name, string surname, string email, ERole role, bool isActive)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Role = role;
        IsActive = isActive;
    }
}
