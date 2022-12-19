using Microsoft.AspNetCore.Components;
using Shared.AuthUsers;
using System.Net.Http.Json;

namespace Client.Users;

public partial class Index
{
    // Class voor Table Component
    public class Gebruiker
    {
        /*        public int Id;
                public string? Voornaam;
                public string? Naam;
                public string? Role;
                public string? Is_Actief;
                public string? Email;*/
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
    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }
    [Parameter, SupplyParameterFromQuery] public string? Role{ get; set; }

    // Andere
    //private List<UserDto.Index>? users = new();
    private IEnumerable<AuthUserDto.Index>? users;

    public ICollection<Gebruiker> userObjects = new List<Gebruiker>();
    private readonly string createUri = "/gebruikers/aanmaken";

    private bool error = false;
    private string errorMessage = string.Empty;
    private bool loading = false;

    private const string endpoint = "AuthUser";

    // Methoden
    protected override async Task OnParametersSetAsync()
    {
        UserRequest.Index request = new()
        {
            Searchterm = Searchterm,
            Page = Page.HasValue ? Page.Value : 1,  
            PageSize = PageSize.HasValue ? PageSize.Value : 25,
            Role = Role,
        };

        await RefreshUsersAsync(request);
    }

    private async Task RefreshUsersAsync(UserRequest.Index request)
    {
        userObjects.Clear();

        try
        {
            loading = true;
            error = false;
            //var response = await UserService.GetIndexAsync(request);
            var response = await Http.GetFromJsonAsync<AuthUserDto.Index[]>($"{endpoint}");
            users = response;

            users?.ToList().ForEach(c =>
            {
                userObjects.Add(new Gebruiker
                {
                    /*Id = c.Id,
                    Naam = c.Name,
                    Voornaam = c.Surname,
                    Role = GiveRoleAsString(c.Role),
                    Is_Actief = c.IsActive ? "Ja" : "Neen",
                    Email = c.Email,*/
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

    private async Task DeleteUserAsync(int id)
    {
        await UserService.DeleteAsync(id);
        await RefreshUsersAsync(new());
    }

    private string GiveRoleAsString(ERole role)
    {
        return (role) switch
        {
            ERole.User => "user",
            ERole.Moderator => "beheerder",
            ERole.Admin => "admin",
            _ => ""
        };
    }
}