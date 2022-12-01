using Domain.Users;
using Domain.VirtualMachines;
using Domain.Activities;
using Fakers.Clients;
using Fakers.VirtualMachines;
using System.Runtime.CompilerServices;

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
        SeedVirtualMachines();
        SeedClients();
        SeedUsers();
    }

    private void SeedVirtualMachines()
    {
        // Vms
        var vms = new VirtualMachineFaker().AsTransient().Generate(20);
        dbContext.VirtualMachines.AddRange(vms);

        // Activities
        Random random = new();
        var types = Enum.GetValues(typeof(EActivity));
        var activities = vms.Select(vm => new Activity(vm, (EActivity) types.GetValue(random.Next(types.Length))));
        dbContext.Activities.AddRange(activities);

        // Requests
        var requests = new VirtualMachineRequestFaker().AsTransient().Generate(10);
        dbContext.VirtualMachineRequests.AddRange(requests);
        dbContext.SaveChanges();
    }

    private void SeedClients()
    {
        var clients = new ClientFaker().AsTransient().Generate(10);
        dbContext.Clients.AddRange(clients);
        dbContext.SaveChanges();
    }

    private void SeedUsers()
    {
        var users = new UserFaker().AsTransient().Generate(10);
        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();
    }
}
