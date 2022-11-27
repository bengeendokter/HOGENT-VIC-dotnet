using Domain.Clients.Users;
using Domain.Users;
using Domain.VirtualMachines;
using Fakers.Common;
using Shared;

namespace Fakers.Clients;

public class ClientFaker : EntityFaker<Client>
{
    public ClientFaker(string locale = "nl") : base(locale)
    {
        //Client c = new Client(phonenumber, department, backupContact, clientType, externaltype, education, clientOrganisation, name, email, passrod, role, active)
        //User user = new User(name, email, password, role, active)
        CustomInstantiator(
            f =>
                new Client(
                    // client
                    f.Phone.PhoneNumber(),
                    f.Commerce.Department(),
                    f.Internet.Email(),
                    f.Random.Enum<EClientType>(),
                    f.Internet.DomainName(),
                    f.Name.JobType(),
                    f.Company.CompanyName(),
                    f.Internet.UserName(),
                    f.Internet.Email(),
                    f.Internet.Password(),
                    f.Random.Enum<ERole>(),
                    f.Random.Bool()
                )
        ); ;
    }
}
