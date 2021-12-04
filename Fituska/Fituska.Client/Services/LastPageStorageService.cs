namespace Fituska.Client.Services;

public class LastPageStorageService
{
    private string _lastPage;
    private readonly NavigationManager _navigationManager;

    public LastPageStorageService(NavigationManager navigationManager)
    {
        _lastPage = "/";
        _navigationManager = navigationManager;
    }

    public void SaveCurrentPage()
    {
        _lastPage = _navigationManager.Uri;
    }

    public void NavigateToLastPage()
    {
        _navigationManager.NavigateTo(_lastPage);
    }
}
