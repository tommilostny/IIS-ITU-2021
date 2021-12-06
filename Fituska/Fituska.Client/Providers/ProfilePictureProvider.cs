using Fituska.Client.Shared;
using Fituska.Shared.Models.File;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Fituska.Client.Providers;

public class ProfilePictureProvider
{
    private const string _defaultPicture = "/person-icon.png";
    private readonly Task<AuthenticationState> _authenticationStateTask;
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _http;
    private readonly Base64ImageService _base64ImageService;
    private readonly FituskaAuthenticationStateProvider _authenticationStateProvider;

    public string ImageSource { get; private set; } = _defaultPicture;

    public ProfilePictureProvider(
        Task<AuthenticationState> authenticationStateTask,
        ILocalStorageService localStorageService,
        HttpClient http,
        Base64ImageService base64ImageService,
        FituskaAuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateTask = authenticationStateTask;
        _localStorageService = localStorageService;
        _http = http;
        _base64ImageService = base64ImageService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<ProfilePictureProvider> Inititialize()
    {
        await LoadProfilePicture();
        return this;
    }

    public async Task ClearCachedImage()
    {
        if (await _localStorageService.ContainKeyAsync(StorageNames.UserPhoto))
        {
            await _localStorageService.RemoveItemAsync(StorageNames.UserPhoto);
        }
        ImageSource = _defaultPicture;
    }

    public async Task LoadProfilePicture(NavMenu? navMenuToNotify = null, Guid? userId = null)
    {
        var authUser = (await _authenticationStateTask).User;
        if (authUser.Identity.IsAuthenticated)
        {
            if (await _localStorageService.ContainKeyAsync(StorageNames.UserPhoto))
            {
                ImageSource = await _localStorageService.GetItemAsStringAsync(StorageNames.UserPhoto);
                navMenuToNotify?.Refresh();
                return;
            }
            if (userId is null)
            {
                userId = new Guid(authUser.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            }
            try
            {
                var fileModel = await _http.GetFromJsonAsync<FileUserModel>(ApiEndpoints.UserIdFileUrl((Guid)userId));
                if (!string.IsNullOrEmpty(fileModel.Name) && fileModel.Content.Length > 0)
                {
                    var base64Image = _base64ImageService.ImageFileToBase64(fileModel.Name, fileModel.Content);
                    await _localStorageService.SetItemAsStringAsync(StorageNames.UserPhoto, base64Image);
                    ImageSource = base64Image;
                    navMenuToNotify?.Refresh();
                    return;
                }
            }
            catch
            {
                if (await _localStorageService.ContainKeyAsync(StorageNames.BearerToken))
                {
                    await _localStorageService.RemoveItemAsync(StorageNames.BearerToken);
                    _authenticationStateProvider.SignOut();
                }
                Console.WriteLine($"Unknown user with ID: {userId}");
            }
        }
        ImageSource = _defaultPicture;
        navMenuToNotify?.Refresh();
    }
}
