using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class VirtualMachineConfiguration : EntityConfiguration<VirtualMachine>
{
    public override void Configure(EntityTypeBuilder<VirtualMachine> builder)
    {
        base.Configure(builder);
        builder.HasOne(vm => vm.Client)
            .WithMany(c => c.VirtualMachines).OnDelete(DeleteBehavior.SetNull);
        builder.Navigation(vm => vm.Client).AutoInclude();
        builder.Property(x => x.StartDate).HasConversion<long>();
        builder.Property(x => x.EndDate).HasConversion<long>();
    }
}
