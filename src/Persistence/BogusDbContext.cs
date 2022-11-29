using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class BogusDbContext : DbContext
{
    //public DbSet<Use> Products => Set<Product>();
    //public DbSet<Tag> Tags => Set<Tag>();
    //public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase(databaseName: "BogusDb");
        optionsBuilder.UseTriggers(options =>
        {
            //options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

