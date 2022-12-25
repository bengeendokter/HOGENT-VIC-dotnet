using Domain.Users;
using Fakers.Common;

namespace Fakers.Clients;

public class ClientFaker : EntityFaker<Client>
{
    public ClientFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f =>
        {
            var lastName = f.Person.LastName;
            var firstName = f.Person.FirstName;

            return new Client(
                    lastName,              // Name
                    firstName,             // Surname
                    f.Internet.Email(firstName, lastName),             // Email
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
