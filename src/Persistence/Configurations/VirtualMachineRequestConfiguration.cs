using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class VirtualMachineRequestConfiguration : IEntityTypeConfiguration<VirtualMachineRequest>
{
    public void Configure(EntityTypeBuilder<VirtualMachineRequest> builder)
    {
        builder.HasOne(vmr => vmr.Client)
            .WithMany(c => c.Requests);
        builder.Navigation(vmr => vmr.Client).AutoInclude();
    }
}
