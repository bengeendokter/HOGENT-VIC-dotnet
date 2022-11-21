namespace Shared.Clients;

public interface IClientService
{
    List<ClientDto.Index> GetAll();
    ClientDto.Detail GetDetail(int id);
    
}
