namespace Shared.Clients;

public interface IUserService
{
    Task<List<UserDto.Index>> GetIndexAsync(ClientRequest.Index request);
    Task<UserDto.Detail> GetDetailAsync(int userId);
    Task<int> CreateAsync(UserDto.Mutate model);
    Task EditAsync(int userId, UserDto.Mutate model);
    Task DeleteAsync(int userId);
}
