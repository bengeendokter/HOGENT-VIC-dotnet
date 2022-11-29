using Shared.Users;
using System.Net.Http.Json;

namespace Client.Users;

public class UserService : IUserService
{
    private readonly HttpClient client;
    private const string endpoint = "api/user";

    public UserService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<int> CreateAsync(UserDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task DeleteAsync(int userId)
    {
        await client.DeleteAsync($"{endpoint}/{userId}");
    }

    public async Task EditAsync(int userId, UserDto.Mutate model)
    {
        await client.PutAsJsonAsync($"{endpoint}/{userId}", model);
    }

    public async Task<UserDto.Detail> GetDetailAsync(int userId)
    {
        var response = await client.GetFromJsonAsync<UserDto.Detail>($"{endpoint}/{userId}");
        return response;
    }

    public async Task<UserResult.Index> GetIndexAsync(UserRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<UserResult.Index>($"{endpoint}?searchTerm={request.Searchterm}&page={request.Page}&pageSize={request.PageSize}&role={request.Role}");
        return response;
    }
}
