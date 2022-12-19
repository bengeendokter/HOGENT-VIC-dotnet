using Domain.Activities;
using Fakers.Common;
using System;

namespace Fakers.Activities;

public class ActivityFaker : EntityFaker<Activity>
{
    public ActivityFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Activity(
                f.Random.Enum<EActivity>(),
                f.Internet.DomainWord(),
                $"{f.Person.FirstName} {f.Person.LastName}",
                0,
                0,
                0
            ))
            .RuleFor(a => a.CPU, (f, a) => GetFakeDataByType(f, a.Type, 1, 64))
            .RuleFor(a => a.RAM, (f, a) => GetFakeDataByType(f, a.Type, 2, 256))
            .RuleFor(a => a.Storage, (f, a) => GetFakeDataByType(f, a.Type, 24, 512));
    }

    private static int GetFakeDataByType(Bogus.Faker f, EActivity type, int start, int end)
    {
        return (type) switch
        {
            EActivity.Added => f.Random.Int(start, end),
            EActivity.Deleted => -f.Random.Int(start, end),
            EActivity.Edited => f.Random.Int(-end, end),
            _ => 0
        };
    }

}
