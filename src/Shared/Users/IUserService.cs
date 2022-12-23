using Shared.AuthUsers;
using Shared.Users;

namespace Shared.Clients;

public interface IUserService
{
    Task<UserResult.Index> GetIndexAsync(UserRequest.Index request);
    Task<UserDto.Detail> GetDetailAsync(int userId);
    Task<int> CreateAsync(UserDto.Mutate model);
    Task<AuthUserDto.Detail.General> EditGeneralAsync(string userId, AuthUserRequest.General request);
    Task DeleteAsync(int userId);
}
