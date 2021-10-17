namespace Fituska.Client.Pages;

public partial class SignIn : ComponentBase
{
    [Inject]
    private HttpClient Http { get; set; }

    [Inject]
    private ILocalStorageService LocalStorageService { get; set; }

    [Inject]
    private FituskaAuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private UserModel _userToSignIn = new();
    private bool _signInSuccess = false;
    private bool _signInFailure = false;

    private async Task SignInAsync()
    {
        _signInFailure = false;
        var httpResponseMessage = await Http.PostAsJsonAsync(ApiEndpoints._signInUrl, _userToSignIn);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var jsonWebToken = await httpResponseMessage.Content.ReadAsStringAsync();
            await LocalStorageService.SetItemAsync("bearerToken", jsonWebToken);
            await AuthenticationStateProvider.SignIn();

            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jsonWebToken);
            _signInSuccess = true;
        }
        else
            _signInFailure = true;
    }
}
