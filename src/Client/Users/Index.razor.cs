using Microsoft.AspNetCore.Components;
using static Client.Clients.Index;

namespace Client.Users;

public partial class Index
{
    [Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    private string? searchTerm;
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    private int? page;
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }
    private int? pageSize;

    private List<UserDto.Index>? users;
    public ICollection<Gebruiker> userObjects = new List<Gebruiker>();
    public ICollection<Gebruiker> filteredUsers = new List<Gebruiker>();

    public class Gebruiker
    {
        public int Id;
        public string? Voornaam;
        public string? Naam;
        public string? Role;
        public string? Is_Actief;
        public string? Email;
    };

    protected override async Task OnParametersSetAsync()
    {
        UserRequest.Index request = new()
        {
            Searchterm = Searchterm,
            Page = Page.HasValue ? Page.Value : 1,
            PageSize = PageSize.HasValue ? PageSize.Value : 25,
        };

        searchTerm = Searchterm;
        page = Page;
        pageSize = PageSize;

        await RefreshUsersAsync(request);
    }

    private async Task RefreshUsersAsync(UserRequest.Index request = null)

    {
        userObjects.Clear();
        filteredUsers.Clear();

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

        filteredUsers = userObjects;
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
    private void GoToCreateUser()
    {
        NavigationManager.NavigateTo("/gebruikers/aanmaken");
    }

    private void GoToEditUser(int id)
    {
        NavigationManager.NavigateTo($"/gebruikers/wijzigen/{id}");
    }

    private async Task DeleteUserAsync(int id)
    {
        await UserService.DeleteAsync(id);
        await RefreshUsersAsync(new UserRequest.Index());
    }

    private void FiltersChanged()
    {

        Dictionary<string, object?> parameters = new();

        parameters.Add("searchTerm", searchTerm);
        parameters.Add("page", page);
        parameters.Add("pageSize", pageSize);

        var uri = NavigationManager.GetUriWithQueryParameters(parameters);
        NavigationManager.NavigateTo(uri);
    }

}