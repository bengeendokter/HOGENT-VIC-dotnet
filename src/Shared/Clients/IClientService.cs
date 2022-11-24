namespace Shared.Clients;

public interface IClientService
{
    List<ClientDto.Index> GetAll(ClientRequest.Index request);
    ClientDto.Detail GetDetail(int id);
    void Delete(int id);
}
