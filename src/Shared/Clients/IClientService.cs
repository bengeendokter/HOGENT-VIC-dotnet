namespace Shared.Clients;

public interface IClientService
{
    List<ClientDto.Index> GetAll(ClientRequest.Index request);
    ClientDto.Detail GetDetail(int clientId);
    int Create(ClientDto.Mutate model);
    void Edit(int clientId, ClientDto.Mutate model);
    void Delete(int clientId);
}
