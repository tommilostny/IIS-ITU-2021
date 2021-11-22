namespace Fituska.Client.Providers;

public class LastPageStorageProvider
{
    private string _lastPage;
    private readonly NavigationManager _navigationManager;

    public LastPageStorageProvider(NavigationManager navigationManager)
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
