namespace Shared.Clients;

public interface IClientService
{
    Task<List<ClientDto.Index>> GetIndexAsync(ClientRequest.Index request);
    Task<ClientDto.Detail> GetDetailAsync(int clientId);
    Task<int> CreateAsync(ClientDto.Mutate model);
    Task EditAsync(int clientId, ClientDto.Mutate model);
    Task DeleteAsync(int clientId);
}
