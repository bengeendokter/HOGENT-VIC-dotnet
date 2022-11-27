using Domain.Clients;
using Domain.Users;
using Fakers.Common;

namespace Fakers.Clients;

public class ClientFaker : EntityFaker<Client>
{
    public ClientFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Client(
                f.Internet.UserName(),          // Name
                f.Internet.Email(),             // Email
                f.Phone.PhoneNumber(),          // PhoneNumber
                f.Phone.PhoneNumber(),          // BackupContact
                f.Random.Enum<EClientType>(),   // ClientType
                f.Commerce.Department(),        // ClientOrganisation
                f.Company.CompanyName(),        // Education
                f.Company.CompanyName()         // ExternalType
            )
        ); 
    }
}
