using Domain.Users;
using Domain.VirtualMachines;
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
    }

    private void SeedVirtualMachines()
    {
        var vms = new VirtualMachineFaker().AsTransient().Generate(20);
        dbContext.VirtualMachines.AddRange(vms);
        dbContext.SaveChanges();
    }

    private void SeedClients()
    {
        var clients = new ClientFaker().AsTransient().Generate(10);
        dbContext.Clients.AddRange(clients);
        dbContext.SaveChanges();
    }
}
