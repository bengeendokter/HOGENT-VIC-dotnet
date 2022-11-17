using Shared.Clients;

namespace Client.Users;

public class ClientService : IClientService
{
    private readonly List<ClientDto.Index> _clients = new();

    public ClientService()
    {
        SetDummyData();
    }

    public List<ClientDto.Index> GetAll()
    {
        throw new NotImplementedException();
    }

    private void SetDummyData()
    {

    }
}
