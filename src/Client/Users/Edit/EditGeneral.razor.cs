using Microsoft.AspNetCore.Components;
using Shared.AuthUsers;
using System.Net.Http.Json;

namespace Client.Users.Edit;

public partial class Edit
{
    [Parameter] public string? Id { get; set; }
    [Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public HttpClient Http { get; set; } = default!;


    private UserDto.Mutate user = new();

/*    protected async override Task OnParametersSetAsync()
    {
        //await base.OnParametersSetAsync();
        //var response = await UserService.GetDetailAsync(Id);
        var response = await Http.GetFromJsonAsync<AuthUserDto.Index[]>("AuthUser");
        *//*        UserDto.Detail detailUser = response;
                user = new UserDto.Mutate()
                {
                    Email = detailUser.Email,
                    IsActive = detailUser.IsActive,
                    Name = detailUser.Name,
                    Surname = detailUser.Surname,
                    Role = detailUser.Role
                };*//*
    }*/

    private async Task EditUserAsync()
    {
        //await UserService.EditAsync(Id, user);
        NavigationManager.NavigateTo("/gebruikers");
    }
}