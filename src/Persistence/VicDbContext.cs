using System.Reflection;
using Domain.VirtualMachines;
using Persistence.Triggers;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Users;

namespace Persistence;

public class VicDbContext : DbContext
{
    public DbSet<VirtualMachine> VirtualMachines => Set<VirtualMachine>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<User> Users => Set<User>();

    public DbSet<VirtualMachineRequest> VirtualMachineRequests => Set<VirtualMachineRequest>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase(databaseName: "VIC");
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
