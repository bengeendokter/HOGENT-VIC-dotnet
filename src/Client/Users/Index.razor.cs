using Microsoft.AspNetCore.Components;

namespace Client.Users;

public partial class Index
{
    // Class voor Table Component
    public class Gebruiker
    {
        public int Id;
        public string? Voornaam;
        public string? Naam;
        public string? Role;
        public string? Is_Actief;
        public string? Email;
    };

    // QueryParameters + Inject (partial class)
    [Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }
    [Parameter, SupplyParameterFromQuery] public string? Role{ get; set; }

    // Andere
    private List<UserDto.Index>? users;
    public ICollection<Gebruiker> userObjects = new List<Gebruiker>();
    private readonly string createUri = "/gebruikers/aanmaken";

    private bool error = false;
    private string errorMessage = string.Empty;

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
            error = false;
            var response = await UserService.GetIndexAsync(request);
            users = response.Users?.ToList();

            users?.ForEach(c =>
            {
                userObjects.Add(new Gebruiker
                {
                    Id = c.Id,
                    Naam = c.Name,
                    Voornaam = c.Surname,
                    Role = GiveRoleAsString(c.Role),
                    Is_Actief = c.IsActive ? "Ja" : "Neen",
                    Email = c.Email,
                });
            });
        }
        catch (Exception ex)
        {
            error = true;
            errorMessage = ex.Message;
        } 
    }

    private void GoToEditUser(int id)
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