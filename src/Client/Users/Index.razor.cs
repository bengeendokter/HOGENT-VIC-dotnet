using Client.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Shared.AuthUsers;
using Shared.Error;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client.Users;

public partial class Index
{
    // Class voor Table Component
    public class Gebruiker
    {
        public string? Id;
        public string? Voornaam;
        public string? Achternaam;
        public string? Actief;
        public string? Email;
    };

    // QueryParameters + Inject (partial class)
    [Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public HttpClient Http { get; set; }
    [Inject] public IJSRuntime JsRuntime { get; set; }
    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }
    [Parameter, SupplyParameterFromQuery] public string? Role{ get; set; }

    // Andere
    //private List<UserDto.Index>? users = new();
    private IEnumerable<AuthUserDto.Index>? users;
    private IEnumerable<AuthUserDto.Detail.UserRole>? roles;

    public ICollection<Gebruiker> userObjects = new List<Gebruiker>();
    private readonly string createUri = "/gebruikers/aanmaken";

    private bool error = false;
    private string errorMessage = string.Empty;

    private bool minorError = false;
    private string minorErrorMessage = String.Empty;


    private bool loading = false;

    private const string endpoint = "AuthUser";

    // Methoden
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        try
        {
            loading = true;
            roles = await Http.GetFromJsonAsync<AuthUserDto.Detail.UserRole[]>($"AuthUser/roles");
            loading = false;
        } catch (Exception ex)
        {
            loading = false;
            error = true;
            errorMessage = ex.Message;
        }


    }

    private string GetRoleIdByName(string name)
    {
        return roles?.First(x => x.Role == name).Id ?? "";
    }
    protected override async Task OnParametersSetAsync()
    {
        AuthUserRequest.Index request = new()
        {
            Searchterm = Searchterm,
            Page = Page ?? 0,  
            PageSize = PageSize ?? 25,
            Role = Role,
        };

        await RefreshUsersAsync(request);
    }

    private async Task RefreshUsersAsync(AuthUserRequest.Index request)
    {
        userObjects.Clear();

        try
        {
            loading = true;
            error = false;
            var response = await Http.GetFromJsonAsync<List<AuthUserDto.Index>>($"{endpoint}/" +
                $"?searchTerm={request.Searchterm}&" +
                $"page={request.Page}&" +
                $"pageSize={request.PageSize}&" +
                $"role={request.Role}");
            users = response;

            users?.ToList().ForEach(c =>
            {
                userObjects.Add(new Gebruiker
                {
                    Id = c.Id,
                    Voornaam = c.FirstName,
                    Achternaam = c.LastName,
                    Email = c.Email,
                    Actief = c.Blocked? "Neen" : "Ja"
                });
            });

            foreach(var user in users) { Console.WriteLine("Ids = ", user.Id); }
            loading = false;
        }
        catch (Exception ex)
        {
            loading = false;
            error = true;
            errorMessage = ex.Message;
        } 
    }

    private void GoToEditUser(string id)
    {
        NavigationManager.NavigateTo($"/gebruikers/wijzigen/{id}");
    }

    private async Task DeleteUserAsync(string id)
    {
        try
        {
            error = false;
            loading = true;
            var response = await Http.DeleteAsync($"AuthUser/{id}");
            if (!response.IsSuccessStatusCode)
            {
                loading = false;
                minorError = true;
                string message = response.Content.ReadAsStringAsync().Result;
                ResponseError error = JsonConvert.DeserializeObject<ResponseError>(message);
                minorErrorMessage = error?.Message ?? "Er gebeurde een ongekende error.";

            } else
            {
                loading = false;
                await RefreshUsersAsync(new());
            }
        } catch (Exception ex)
        {
            loading = false;
            error = true;
            errorMessage = ex.Message;
        }
    }

    private void ResetMinorError()
    {
        minorError = false;
        minorErrorMessage = String.Empty;
    }
}