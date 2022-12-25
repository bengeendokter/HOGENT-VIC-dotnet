using Domain.VirtualMachines;
using Fakers.Clients;
using Fakers.Common;

namespace Fakers.VirtualMachines;

public class VirtualMachineFaker : EntityFaker<VirtualMachine>
{
    public VirtualMachineFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(
            f =>
                new VirtualMachine(
                    f.Internet.DomainWord(),
                    f.Internet.DomainName(),
                    f.Date.Future(1, DateTime.Now.AddDays(1)).ToUniversalTime(),
                    f.Date.Future(1, DateTime.Now.AddYears(1)).ToUniversalTime(),
                    f.Internet.Ip(),
                    "80, 443",
                    f.Random.Enum<ETemplate>(),
                    $"{f.Address.Country().Substring(0, 4)}-{f.Random.Int(0, 500)}.hogent.be",
                    f.Random.Int(1, 64),
                    f.Random.Int(2, 256),
                    f.Random.Int(24, 512),
                    f.Random.Enum<ESoftware>(),
                    f.Random.Enum<EBackupFrequency>(),
                    (EDay)f.Random.Int(1, 127),
                    f.Random.Bool(),
                    f.Random.Bool(),
                    new ClientFaker(locale).AsTransient()
                )
        );
    }
}
