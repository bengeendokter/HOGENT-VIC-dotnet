using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class VirtualMachineRequestConfiguration : EntityConfiguration<VirtualMachineRequest>
{
    public override void Configure(EntityTypeBuilder<VirtualMachineRequest> builder)
    {
        base.Configure(builder);
        builder.HasOne(vmr => vmr.Client)
            .WithMany(c => c.Requests);
        builder.Navigation(vmr => vmr.Client).AutoInclude();
        builder.Property(x => x.StartDate).HasConversion<long>();
        builder.Property(x => x.EndDate).HasConversion<long>();
    }
}
