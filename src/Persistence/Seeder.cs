using Domain.Clients.Users;
using Domain.VirtualMachines;
using Fakers.Clients;
using Fakers.VirtualMachines;

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
        SeedUsers();
        SeedClients();
    }

    private void SeedVirtualMachines()
    {
        var vms = new VirtualMachineFaker().AsTransient().Generate(20);
        dbContext.VirtualMachines.AddRange(vms);
        dbContext.SaveChanges();
    }

    private void SeedUsers()
    {
        var users = new UserFaker().AsTransient().Generate(10);
        dbContext.Users.AddRange(users);
        //dbContext.Users.AddRange(users);
        dbContext.SaveChanges();
    }

    private void SeedClients()
    {
        var clients = new ClientFaker().AsTransient().Generate(10);
        dbContext.Users.AddRange(clients);
        dbContext.SaveChanges();
    }
}
