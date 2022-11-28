using Domain.Users;
using Domain.VirtualMachines;
using Fakers.Common;


namespace Fakers.Clients;

public class UserFaker : EntityFaker<User>
{
    public UserFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(
            f =>
                new User(
                    f.Person.LastName!,         // Name
                    f.Person.FirstName,         // Surname
                    f.Internet.Email(),         // Email
                    f.Random.Enum<ERole>(),     // Role
                    f.Random.Bool()             // IsActive
                )
        );
    }
}
