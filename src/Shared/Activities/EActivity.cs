using System.ComponentModel;

namespace Shared.Activities;

public enum EActivity
{
    Deleted,
    Added,
    Edited
}

public static class EActivityExtensions
{
    public static string ToDisplayName(this EActivity type)
    {
        return (type) switch
        {
            EActivity.Deleted => "VM verwijderd",
            EActivity.Added => "VM toegevoegd",
            EActivity.Edited => "VM aangepast"
        };
    }
}
