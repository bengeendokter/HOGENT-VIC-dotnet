using Microsoft.AspNetCore.Components;
using Shared.AuthUsers;
using System.Net;
using System.Net.Http.Json;
using static Shared.AuthUsers.AuthUserDto.Detail;
using static Shared.AuthUsers.AuthUserRequest;

namespace Client.Users.Edit;

public partial class EditGeneral
{
    [Parameter] public string? Id { get; set; }
    //[Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public HttpClient Http { get; set; } = default!;

    private AuthUserDto.Mutate.General? _userGeneralInfoMutate = new();
    private AuthUserDto.Detail.General? _userGeneralInfo = new();

    private bool error = false;
    private string errorMessage = string.Empty;
    private bool loading = false;

    protected async override Task OnParametersSetAsync()
    { 
        try
        {

            loading = true;
            await base.OnParametersSetAsync();
            var response = await Http.GetFromJsonAsync<AuthUserDto.Detail.General>($"AuthUser/{Id}");
            _userGeneralInfo = response;
            SetDetailsInUser();
            loading = false;
        }
        catch (Exception ex)
        {
            loading = false;
            error = true;
            errorMessage = ex.Message;
        }
    }

    private void SetDetailsInUser()
    {
        _userGeneralInfoMutate.Email = _userGeneralInfo.Email;
        _userGeneralInfoMutate.FirstName = _userGeneralInfo.FirstName;
        _userGeneralInfoMutate.LastName = _userGeneralInfo.LastName;
        _userGeneralInfoMutate.ScreenName = _userGeneralInfo.ScreenName;
        _userGeneralInfoMutate.Blocked = !_userGeneralInfo.Blocked;
    }

    private async Task OnSubmit()
    {

        try
        {
            loading = true;

            var request = new AuthUserRequest.General()
            {
                Blocked = !_userGeneralInfoMutate.Blocked,
                Email = _userGeneralInfoMutate.Email,
                FirstName = _userGeneralInfoMutate.FirstName,
                LastName = _userGeneralInfoMutate.LastName,
                ScreenName = _userGeneralInfoMutate.ScreenName
            };

            var response = await Http.PutAsJsonAsync($"/AuthUser/{Id}", request);

            loading = false;
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo($"/gebruikers/wijzigen/{Id}");
            }


        } catch (WebException ex)
        {
            loading = false;
            error = true;
            errorMessage = ex.Message;
        } 
    }
}