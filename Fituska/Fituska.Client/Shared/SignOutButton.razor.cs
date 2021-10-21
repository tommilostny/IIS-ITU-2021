namespace Fituska.Client.Shared;

public partial class SignOutButton : ComponentBase
{
    [Inject]
    private ILocalStorageService LocalStorageService { get; set; }

    [Inject]
    private FituskaAuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private async Task SignOutAsync()
    {
        if (await LocalStorageService.ContainKeyAsync(JwtNames.BearerToken))
        {
            await LocalStorageService.RemoveItemAsync(JwtNames.BearerToken);
            AuthenticationStateProvider.SignOut();
        }
        StateHasChanged();
        NavigationManager.NavigateTo("/");
    }
}
