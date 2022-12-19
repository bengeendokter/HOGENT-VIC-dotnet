using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class VirtualMachineConfiguration : IEntityTypeConfiguration<VirtualMachine>
{
    public void Configure(EntityTypeBuilder<VirtualMachine> builder)
    {
        builder.HasOne(vm => vm.Client)
            .WithMany(c => c.VirtualMachines);
        builder.Navigation(vm => vm.Client).AutoInclude();
    }
}
