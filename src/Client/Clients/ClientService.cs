//using Bogus.DataSets;

namespace Client.Users;

public class ClientService : IClientService
{
    private readonly List<ClientDto.Detail> _clients = new();

    public ClientService()
    {
        SetDummyData();
    }

    public List<ClientDto.Index> GetAll(ClientRequest.Index request)
    {
        return _clients.Select(x => new ClientDto.Index
        {
            Id = x.Id,
            Name = x.Name,
            PhoneNumber = x.PhoneNumber,
            ClientType = x.ClientType,
            ClientOrganisation = x.ClientOrganisation,
        }).ToList();
    }

    public ClientDto.Detail GetDetail(int clientId)
    {
        var client = _clients.First(x => x.Id == clientId);

        return new ClientDto.Detail
        {
            Id = client.Id,
            Name = client.Name,
            PhoneNumber = client.PhoneNumber,
            ClientType = client.ClientType,
            ClientOrganisation = client.ClientOrganisation,
            Email = client.Email,
            BackupContact = client.BackupContact,
            Education = client.Education,
            ExternalType = client.ExternalType,
        };
    }

    public int Create(ClientDto.Mutate model)
    {
        if (_clients.Any(x => x.Name == model.Name))
            throw new Exception();

        int id = _clients.Count + 1;
        ClientDto.Detail client = new ClientDto.Detail
        {
            Id = id,
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            BackupContact = model.BackupContact,
            ClientOrganisation = model.ClientOrganisation,
            ClientType = model.ClientType,
            Education = model.Education,
            ExternalType= model.ExternalType,
        };

        _clients.Add(client);
        return id;
    }

    public void Edit(int clientId, ClientDto.Mutate model)
    {
        ClientDto.Detail? client = _clients.FirstOrDefault(x => x.Id == clientId);
        if (client == null)
            throw new Exception();

        client.Name = model.Name;
        client.PhoneNumber = model.PhoneNumber;
        client.Email = model.Email;
        client.BackupContact = model.BackupContact;
        client.ClientOrganisation = model.ClientOrganisation;
        client.ClientType = model.ClientType;
        client.Education = model.Education;
        client.ExternalType = model.ExternalType;
    }


    public void Delete(int clientId)
    {
        var client = _clients.FirstOrDefault(x => x.Id == clientId);
        if (client == null)
            return;

        _clients.Remove(client);
    }


    private void SetDummyData()
    {
        _clients.AddRange(new List<ClientDto.Detail>
        {
            new ClientDto.Detail
            {
                Id = 1,
                Name = "Marc Asselberg",
                Email = "marc.asselberg@hogent.be",
                PhoneNumber = "123 45 67 89",
                BackupContact = "marc.asselberg@hogent.be",
                ClientOrganisation = "DIT",
                ClientType = EClientType.Internal,
                Education = "Toegepaste Informatica"

            },
            new ClientDto.Detail
            {
                Id = 2,
                Name = "Danny Van Assche",
                Email = "dannyvanassche@unizo.be",
                PhoneNumber = "123 45 67 89",
                ClientType = EClientType.External,
                BackupContact = "dannyvanassche@unizo.be",
                ClientOrganisation = "UNIZO",
                ExternalType = "?"
            },
            new ClientDto.Detail
            {
                Id = 3,
                Name = "Liesbeth Van Coppenolle",
                Email = "liesbeth.vancoppenolle@hogent.be",
                PhoneNumber = "123 45 67 89",
                ClientType = EClientType.Internal,
                BackupContact = "liesbeth.vancoppenolle@hogent.be",
                ClientOrganisation = "Gezondheidszorg",
                Education = "Verpleegkunde"
            },
            new ClientDto.Detail
            {
                Id = 4,
                Name = "Kaat Bossuyt",
                Email = "kaat.bossuyt@hogent.be",
                PhoneNumber = "123 45 67 89",
                ClientType = EClientType.Internal,
                BackupContact = "kaat.bossuyt@hogent.be",
                ClientOrganisation = "Biowetenschappen",
                Education = "Chemie"
            },
            new ClientDto.Detail
            {
                Id = 5,
                Name = "Wouter De Geest",
                Email = "wouterdegeest@voka.be",
                PhoneNumber = "123 45 67 89",
                ClientType = EClientType.External,
                BackupContact = "wouterdegeest@voka.be",
                ClientOrganisation = "VOKA",
                ExternalType = "?"
            }
        });
    }
}
