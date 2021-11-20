using Fituska.Shared.Models.User;

namespace Fituska.Client.Pages;

public partial class Register : ComponentBase
{
    [Inject]
    private HttpClient Http { get; set; }

    private readonly UserRegistrationModel _userToRegister = new();
    private bool _registerSuccess = false;
    private bool _registerFailure = false;
    private string? _registerErrorMessage = null;

    private async Task RegisterAsync()
    {
        _registerFailure = false;
        var httpResponseMessage = await Http.PostAsJsonAsync(ApiEndpoints.RegisterUrl, _userToRegister);

        if (httpResponseMessage.IsSuccessStatusCode)
            _registerSuccess = true;
        else
        {
            var serverErrorMessages = await httpResponseMessage.Content.ReadAsStringAsync();
            _registerErrorMessage = $"{serverErrorMessages} Zkontrolujte zadané údaje.";
            _registerFailure = true;
        }
    }

    private void ImageUpload(string imageUrl)
    {
        _userToRegister.PhotoUrl = imageUrl;
    }
}
