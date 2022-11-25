using Ardalis.GuardClauses;
using Domain.Common;
using Domain.VirtualMachines;
using Shared;

namespace Domain.Clients.Users;

public class User : Entity
{
    private string name = default!;
    public string Name
    {
        get => name;
        set => name = Guard.Against.NullOrEmpty(value, nameof(Name));
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

    private string password = default!;
    public string Password
    {
        get => password;
        set => password = Guard.Against.NullOrWhiteSpace(value, nameof(password));
    }

    private User() { }

    public User(string name, string email, string password, ERole role, bool isActive)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        IsActive = isActive;
    }
}
