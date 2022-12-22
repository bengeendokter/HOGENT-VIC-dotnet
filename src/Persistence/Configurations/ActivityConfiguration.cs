using Domain.Activities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ActivityConfiguration : EntityConfiguration<Activity>
{
    public override void Configure(EntityTypeBuilder<Activity> builder)
    {
        base.Configure(builder);
        //builder.HasOne(a => a.VirtualMachine);
        //builder.Navigation(a => a.VirtualMachine).AutoInclude();
    }
}
