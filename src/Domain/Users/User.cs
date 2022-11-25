using Ardalis.GuardClauses;
using Domain.Common;
using Shared;

namespace Domain.Users;

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
}
