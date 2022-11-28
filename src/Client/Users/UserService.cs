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

    public Task DeleteAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(int userId, UserDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto.Detail> GetDetailAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResult.Index> GetIndexAsync(UserRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<UserResult.Index>($"{endpoint}?searchTerm={request.Searchterm}&page={request.Page}&pageSize={request.PageSize}");
        return response;
    }
}
