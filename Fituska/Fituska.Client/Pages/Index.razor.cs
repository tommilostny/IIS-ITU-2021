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
        if (await LocalStorageService.ContainKeyAsync(JwtNames.BearerToken))
        {
            var savedToken = await LocalStorageService.GetItemAsync<string>(JwtNames.BearerToken);
            await AuthenticationStateProvider.SignIn();

            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtNames.Bearer, savedToken);
            StateHasChanged();
        }
        await base.OnInitializedAsync();
    }
}
