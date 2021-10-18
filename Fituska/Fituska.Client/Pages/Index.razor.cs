namespace Fituska.Client.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    private HttpClient Http { get; set; }

    [Inject]
    private FituskaAuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    private ILocalStorageService LocalStorageService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (await LocalStorageService.ContainKeyAsync(JwtNames._bearerToken))
        {
            var savedToken = await LocalStorageService.GetItemAsync<string>(JwtNames._bearerToken);
            await AuthenticationStateProvider.SignIn();

            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtNames._bearer, savedToken);
            StateHasChanged();
        }
        await base.OnInitializedAsync();
    }
}
