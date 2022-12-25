using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ClientConfiguration : EntityConfiguration<Client>
{
    public override void Configure(EntityTypeBuilder<Client> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasMany(c => c.VirtualMachines)
            .WithOne(vm => vm.Client).OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(c => c.Requests)
            .WithOne(vmr => vmr.Client);
    }
}


