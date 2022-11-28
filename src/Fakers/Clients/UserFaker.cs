using Domain.Clients.Users;
using Domain.Users;
using Domain.VirtualMachines;
using Fakers.Common;
using Shared;

namespace Fakers.Clients;

public class UserFaker : EntityFaker<User>
{
    public UserFaker(string locale = "nl") : base(locale)
    {
        //User user = new User(name, email, password, role, active)
        CustomInstantiator(
            f =>
                new User(
                    // client
                    f.Internet.UserName(),
                    f.Internet.Email(),
                    //f.Internet.Password(),
                    f.Random.Enum<ERole>()
                    //f.Random.Bool()
                )
        );
    }
}
