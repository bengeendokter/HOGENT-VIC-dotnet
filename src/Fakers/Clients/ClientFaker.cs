using Domain.Clients;
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
        CustomInstantiator(f => new Client(
                f.Internet.UserName(),          // Name
                f.Internet.Email(),             // Email
                f.Phone.PhoneNumber("+## ### ## ## ##"),          // PhoneNumber
                f.Phone.PhoneNumber("+## ### ## ## ##"),          // BackupContact
                f.Random.Enum<EClientType>(),   // ClientType
                f.Company.CompanyName(),        // ClientOrganisation
                f.Company.CompanyName(),        // Education
                f.Company.CompanyName()         // ExternalType
            )
        ); 
    }
}
