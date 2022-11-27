using System.ComponentModel;

namespace Shared;

public enum ERole
{
    [Description("user")]
    User,
    [Description("moderator")]
    Moderator,
    [Description("admin")]
    Admin,
}
