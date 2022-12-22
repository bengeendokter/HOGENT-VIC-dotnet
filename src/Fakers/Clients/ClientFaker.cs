using Domain.Users;
using Domain.VirtualMachines;
using Fakers.Common;

namespace Fakers.Clients;

public class ClientFaker : EntityFaker<Client>
{
    public ClientFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Client(
                f.Person.LastName,              // Name
                f.Person.FirstName,             // Surname
                f.Random.Bool()? f.Internet.Email() : "yigit@gmail.com",             // Email
                f.Phone.PhoneNumber("+## ### ## ## ##"),          // PhoneNumber
                f.Phone.PhoneNumber("+## ### ## ## ##"),          // BackupContact
                f.Random.Enum<EClientType>(),   // ClientType
                f.Company.CompanyName(),        // ClientOrganisation
                f.Company.CompanyName(),        // Education
                f.Company.CompanyName(),        // ExternalType
                f.Random.Bool()                 // IsActive
            )
        ); 
    }
}
