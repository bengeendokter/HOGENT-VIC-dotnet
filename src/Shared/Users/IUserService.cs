using Shared.Users;

namespace Shared.Clients;

public interface IUserService
{
    Task<UserResult.Index> GetIndexAsync(UserRequest.Index request);
    Task<UserDto.Detail> GetDetailAsync(int userId);
    Task<int> CreateAsync(UserDto.Mutate model);
    Task EditAsync(int userId, UserDto.Mutate model);
    Task DeleteAsync(int userId);
}
