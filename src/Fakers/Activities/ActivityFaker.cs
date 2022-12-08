using Domain.Activities;
using Fakers.Common;
using Fakers.VirtualMachines;

namespace Fakers.Activities;

public class ActivityFaker : EntityFaker<Activity>
{
    public ActivityFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Activity(
                new VirtualMachineFaker(locale).AsTransient(),
                f.Random.Enum<EActivity>()
            )
        );
    }

}
