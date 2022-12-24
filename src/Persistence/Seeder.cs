using Fakers.Clients;
using Fakers.VirtualMachines;
using Fakers.Activities;
using Domain.Users;
using Fakers.Common;

namespace Persistence;

public class Seeder
{
    private readonly VicDbContext dbContext;

    public Seeder(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        SeedVirtualMachines();
        SeedClients();
        SeedActivities();
    }

    private void SeedVirtualMachines()
    {
        // Vms
        var vms = new VirtualMachineFaker().AsTransient().Generate(20);
        dbContext.VirtualMachines.AddRange(vms);

        // Requests
        var requests = new VirtualMachineRequestFaker().AsTransient().Generate(20);
        dbContext.VirtualMachineRequests.AddRange(requests);
        dbContext.SaveChanges();
    }

    private void SeedClients()
    {
        var clients = new ClientFaker().AsTransient().Generate(10);
        var jelle = new JelleFaker().AsTransient().Generate(1);

        dbContext.Clients.AddRange(clients);
        dbContext.Clients.AddRange(jelle);
        dbContext.SaveChanges();
    }

    private void SeedActivities()
    {
        var activities = new ActivityFaker().AsTransient().Generate(25);
        dbContext.Activities.AddRange(activities);
        dbContext.SaveChanges();
    }
}


