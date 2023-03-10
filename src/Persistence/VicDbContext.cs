using Domain.Activities;
using Domain.Users;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Triggers;

namespace Persistence;

public class VicDbContext : DbContext
{
    public DbSet<VirtualMachine> VirtualMachines => Set<VirtualMachine>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<VirtualMachineRequest> VirtualMachineRequests => Set<VirtualMachineRequest>();

    public VicDbContext(DbContextOptions<VicDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VicDbContext).Assembly);
    }
}