using Domain.Common;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Domain.VirtualMachines;

public class HostString : ValueObject
{
    public string Value { get; } = default!;

    private HostString() { }

    public HostString(string value)
    {
        if (!IsValid(value))
            throw new ApplicationException($"Invalid {nameof(HostString)}: {value}");
    }

    private static bool IsValid(string host)
    {
        return Regex.IsMatch(host.ToLower(), @"^([a - z]{ 4})\-([0 - 9]{ 1,10})\.hogent\.be$");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}
