using Domain.VirtualMachines;
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
    }

    private void SeedVirtualMachines()
    {
        var vms = new VirtualMachineFaker().AsTransient().Generate(20);
        dbContext.VirtualMachines.AddRange(vms);
        dbContext.SaveChanges();
    }
}
