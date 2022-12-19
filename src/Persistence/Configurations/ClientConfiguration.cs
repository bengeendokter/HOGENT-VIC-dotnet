using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasMany(c => c.VirtualMachines)
            .WithOne(vm => vm.Client);
        builder.HasMany(c => c.Requests)
            .WithOne(vmr => vmr.Client);
    }
}
