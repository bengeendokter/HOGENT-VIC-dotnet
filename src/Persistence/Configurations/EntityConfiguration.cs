using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations;

internal class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // All tables are singlular named and have the name of the Entity
        builder.ToTable(typeof(T).Name);
        // All entities can be soft deleted but are not by default
        builder.Property(x => x.IsEnabled).IsRequired().HasDefaultValue(true).ValueGeneratedNever();
        // Default MySQL constraint for CreatedAt and UpdatedAt
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("UTC_TIMESTAMP()");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("UTC_TIMESTAMP()").IsConcurrencyToken();
    }
}
