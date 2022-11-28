using System.Net.Http.Json;

namespace Client.Users;

public class ClientService : IClientService
{
    private readonly HttpClient client;
    private const string endpoint = "api/client";

    public ClientService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<ClientDto.Index>> GetIndexAsync(ClientRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<List<ClientDto.Index>>($"{endpoint}?{request.GetQueryString()}");
        return response;
    }

    public async Task<ClientDto.Detail> GetDetailAsync(int clientId)
    {
        var response = await client.GetFromJsonAsync<ClientDto.Detail>($"{endpoint}/{clientId}");
        return response;
    }

    public async Task<int> CreateAsync(ClientDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task EditAsync(int clientId, ClientDto.Mutate model)
    {
        await client.PutAsJsonAsync($"{endpoint}/{clientId}", model);
    }

    public async Task DeleteAsync(int clientId)
    {
        await client.DeleteAsync($"{endpoint}/{clientId}");
    }

}
