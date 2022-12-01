using Microsoft.AspNetCore.Components;

namespace Client.Users;

public partial class Create
{
    private UserDto.Mutate user = new();

    [Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    public async Task CreateUserAsync()
    {
        int userId = await UserService.CreateAsync(user);
        NavigationManager.NavigateTo("/gebruikers");
    }

}