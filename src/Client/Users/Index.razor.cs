using Microsoft.AspNetCore.Components;
using static Client.Clients.Index;

namespace Client.Users;

public partial class Index
{
    [Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }

    private ICollection<UserDto.Index>? users;
    //private ICollection<Klant> clientObjects = new List<Klant>();
    //public ICollection<Klant> filteredClients = new List<Klant>();

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

        var response = await UserService.GetIndexAsync(request);
        users = response.Users?.ToList();
    }

    private void GoToCreateUser()
    {
        NavigationManager.NavigateTo("/gebruikers/aanmaken");
    }

    private void GoToEditUser(int id)
    {
        NavigationManager.NavigateTo($"/gebruikers/wijzigen/{id}");
    }

    private async void DeleteUserAsync(int id)
    {
        await UserService.DeleteAsync(id);
        NavigationManager.NavigateTo("/gebruikers");
    }

}