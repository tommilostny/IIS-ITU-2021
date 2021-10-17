namespace Fituska.Client.Pages;

public partial class Register : ComponentBase
{
    [Inject]
    private HttpClient Http { get; set; }

    private UserModel _userToRegister = new()
    {
        EmailAddress = "xlogin01@vutbr.cz"
    };
    private bool _registerSuccess = false;
    private bool _registerFailure = false;
    private string? _registerErrorMessage = null;

    private async Task RegisterAsync()
    {
        _registerFailure = false;
        var httpResponseMessage = await Http.PostAsJsonAsync(ApiEndpoints._registerUrl, _userToRegister);

        if (httpResponseMessage.IsSuccessStatusCode)
            _registerSuccess = true;
        else
        {
            var serverErrorMessages = await httpResponseMessage.Content.ReadAsStringAsync();
            _registerErrorMessage = $"{serverErrorMessages} Zkontrolujte zadané údaje.";
            _registerFailure = true;
        }
    }
}
