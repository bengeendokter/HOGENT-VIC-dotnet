﻿using Ardalis.GuardClauses;
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

    public User(string name, string email, ERole role)
    {
        Name = name;
        Email = email;
        Role = role;
    }
}
