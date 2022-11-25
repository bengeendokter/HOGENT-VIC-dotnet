using Domain.Users;
using Domain.VirtualMachines;
using Shared;
using System.Runtime.Loader;

namespace Persistence;

public class FakeSeeder
{
    private readonly BogusDbContext dbContext;

    public FakeSeeder(BogusDbContext dbContext)
	{
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        //SeedProducts();
        //SeedUsers();
    }
    /*
    private void SeedProducts()
	{
        var products = new ProductFaker().AsTransient().UseSeed(1337).Generate(100);
        dbContext.Products.AddRange(products);
        dbContext.SaveChanges();
    }
    */
    private void SeedTags()
    {
        List<User> users = new()
        {
            new User("Yigit", "IT", "123456789", ERole.User, virtualMachines: new List<VirtualMachine>()
            {
                new VirtualMachine(),
                new VirtualMachine()
            }, true)
        };

        dbContext.Tags.AddRange(tags);
        dbContext.SaveChanges();
    }
}

