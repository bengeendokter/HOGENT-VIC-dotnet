using Domain.Users;
using Fakers.Common;

namespace Fakers.Clients;


public class JelleFaker : EntityFaker<Client>
{
    public JelleFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f =>
        {
            var lastName = f.Person.LastName;
            var firstName = f.Person.FirstName;

            return new Client(
                    "jelle",              // Name
                    "delporte",             // Surname
                    "jelledelporte0@gmail.com",           // Email
                    f.Phone.PhoneNumber("+## ### ## ## ##"),          // PhoneNumber
                    f.Phone.PhoneNumber("+## ### ## ## ##"),          // BackupContact
                    f.Random.Enum<EClientType>(),   // ClientType
                    f.Company.CompanyName(),        // ClientOrganisation
                    f.Random.CollectionItem(new List<string>() { "Toegepaste Informatica", "Chemie", "Elektro-mechanica", "Biotechnologie" }),        // Education
                    f.Random.CollectionItem(new List<string>() { "Manager", "CEO", "Developer", "Researcher" })        // ExternalType
                );
        }
        );
    }

}

