using Domain.VirtualMachines;
using Shouldly;

namespace Domain.Tests;

public class VirtualMachine_Should
{
    private ETemplate template = ETemplate.DockerLinux;
    private ESoftware software = ESoftware.Docker;
    private EBackupFrequency backupFrequency = EBackupFrequency.Daily;
    private EDay day = EDay.Friday;

    public static IEnumerable<object?[]> TestStringData
    {
        get
        {
            yield return new object?[] { null, null, null, null, null }; // all null
            yield return new object?[] { "", "", "", "", "" }; // all empty
            yield return new object?[] { "name", "", "", "", "" };
            yield return new object?[] { "name", "hostName", "", "", "" };
            yield return new object?[] { "name", "hostName", "fqdn", "", "" };
            yield return new object?[] { "name", "hostName", "fqdn", "ports", "" };
            yield return new object?[] { "name", "hostName", "fqdn", "ports", "host" };
            yield return new object?[] { "name", null, null, null, null };
            yield return new object?[] { "name", "hostName", null, null, null };
            yield return new object?[] { "name", "hostName", "fqdn", null, null };
            yield return new object?[] { "name", "hostName", "fqdn", "ports", null };
            yield return new object?[] { "name", "hostName", "fqdn", "ports", "host" };
            // permutate...
        }
    }

    public static IEnumerable<object?[]> TestDateData
    {
        get
        {
            yield return new object?[] { DateTime.MaxValue, DateTime.MinValue }; // start > end
            yield return new object?[] { DateTime.MinValue, DateTime.Now }; // start < now
        }
    }

    public static IEnumerable<object?[]> TestResourceData
    {
        get
        {
            yield return new object?[] { 0, 0, 0 }; // 0
            yield return new object?[] { -1, -2, -3 }; // negative
            yield return new object?[] { 0, 2, 3 }; // cpu 0
            yield return new object?[] { 1, 0, 3 }; // ram 0
            yield return new object?[] { 1, 2, 0 }; // storage 0
        }
    }

    [Theory]
    [MemberData(nameof(TestStringData))]
    public void Not_exist_when_invalid_string_parameters(string name, string hostName, string fqdn, string ports, string host)
    {
        Should.Throw<ArgumentException>(() => {
            new VirtualMachine(name, hostName, DateTime.Now, DateTime.Now.AddDays(1), fqdn, ports, template, host, 1, 1, 1, software, backupFrequency, day, false, false, null);
        });
    }

    [Theory]
    [MemberData(nameof(TestDateData))]
    public void Not_exist_when_invalid_date_parameters(DateTime startDate, DateTime endDate)
    {
        Should.Throw<ArgumentException>(() => {
            new VirtualMachine("name", "hostName", startDate, endDate, "fqdn", "ports", template, "host", 1, 1, 1, software, backupFrequency, day, false, false, null);
        });
    }

    [Theory]
    [MemberData(nameof(TestResourceData))]
    public void Not_exist_when_invalid_resource_parameters(int cpu, int ram, int storage)
    {
        Should.Throw<ArgumentException>(() => {
            new VirtualMachine("name", "hostName", DateTime.Now, DateTime.Now.AddDays(1), "fqdn", "ports", template, "host", cpu, ram, storage, software, backupFrequency, day, false, false, null);
        });
    }
}