using Microsoft.AspNetCore.Components;

namespace Client.Users;

public partial class Edit
{
    [Parameter] public int Id { get; set; }
    [Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;


    private UserDto.Mutate user = new();

    protected async override Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        var response = await UserService.GetDetailAsync(Id);

        UserDto.Detail detailUser = response;
        user = new UserDto.Mutate()
        {
            Email = detailUser.Email,
            IsActive = detailUser.IsActive,
            Name = detailUser.Name,
            Surname = detailUser.Surname,
            Role = detailUser.Role
        };
    }

    private async Task EditUserAsync()
    {
        await UserService.EditAsync(Id, user);
        NavigationManager.NavigateTo("/gebruikers");
    }
}