using Domain.VirtualMachines;
using Fakers.Common;

namespace Fakers.VirtualMachines;

public class VirtualMachineFaker : EntityFaker<VirtualMachine>
{
    public VirtualMachineFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(
            f =>
                new VirtualMachine(
                    // client
                    f.Internet.DomainWord(),
                    f.Internet.DomainName(),
                   f.Date.Future(1, DateTime.Now.AddDays(1)),
                   f.Date.Future(1, DateTime.Now.AddYears(1)),
                    f.Internet.Ip(),
                    "80, 443",
                    f.Random.Enum<ETemplate>(),
                    f.Internet.Ip(),
                    f.Random.Int(1, 64),
                    f.Random.Int(2, 256),
                    f.Random.Int(24, 512),
                    f.Random.Enum<EMode>(),
                    f.Random.Enum<EBackupFrequency>(),
                    (EDay)f.Random.Int(1, 127),
                    f.Random.Bool(),
                    f.Random.Bool()
                )
        );
    }
}
