using Fituska.Shared.Models.User;

namespace Fituska.Client.Pages;

public partial class UserEdit : ComponentBase
{
    [CascadingParameter]
    protected Task<AuthenticationState>? AuthenticationState { get; set; }

    [Inject]
    private HttpClient Http { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private UserEditModel _userToEdit = null;
    private bool _editFailure = false;
    private string _editErrorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var identityUser = (await AuthenticationState!).User.Identity;
        if (identityUser.IsAuthenticated)
        {
            _userToEdit = await Http.GetFromJsonAsync<UserEditModel>(ApiEndpoints.UserUrl(identityUser.Name));
        }
        else
        {
            NavigationManager.NavigateTo("/signin");
        }
        await base.OnInitializedAsync();
    }

    private async Task UpdateAsync()
    {
        _editFailure = false;
        var httpResponseMessage = await Http.PutAsJsonAsync(ApiEndpoints.UserBaseUrl, _userToEdit);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo($"/user/{_userToEdit.UserName}");
        }
        else
        {
            var serverErrorMessages = await httpResponseMessage.Content.ReadAsStringAsync();
            _editErrorMessage = $"{serverErrorMessages} Zkontrolujte zadané údaje.";
            _editFailure = true;
        }
    }
}
