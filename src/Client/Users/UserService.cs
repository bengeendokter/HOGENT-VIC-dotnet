using Client.Components;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Shared.AuthUsers;
using Shared.Error;
using Shared.Users;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace Client.Users;

public class UserService : IUserService
{
    private readonly HttpClient client;
    private const string endpoint = "AuthUser";

    public UserService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<UserResult.Index> GetIndexAsync(UserRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<UserResult.Index>($"{endpoint}?searchTerm={request.Searchterm}&page={request.Page}&pageSize={request.PageSize}&role={request.Role}");
        return response;
    }

    public async Task<UserDto.Detail> GetDetailAsync(int userId)
    {
        var response = await client.GetFromJsonAsync<UserDto.Detail>($"{endpoint}/{userId}");
        return response;
    }

    public async Task<int> CreateAsync(UserDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task<AuthUserDto.Detail.General> EditGeneralAsync(string userId, AuthUserRequest.General request)
    {
        var response = await client.PutAsJsonAsync($"/AuthUser/{userId}", request);
        if (!response.IsSuccessStatusCode)
        {
            string message = response.Content.ReadAsStringAsync().Result;
            ResponseError error = JsonConvert.DeserializeObject<ResponseError>(message);
            string errorMessage = error?.Message ?? "Er gebeurde een ongekende error.";
            throw new ApplicationException(errorMessage);
        } else
        {
            return await response.Content.ReadFromJsonAsync<AuthUserDto.Detail.General>();
        }

        
    }

    public async Task DeleteAsync(int userId)
    {
        await client.DeleteAsync($"{endpoint}/{userId}");
    }
}
