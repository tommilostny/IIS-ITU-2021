namespace Fituska.Client.Providers;

public class LastPageStorageProvider
{
    private string lastPage;
    private readonly NavigationManager navigationManager;

    public LastPageStorageProvider(NavigationManager navigationManager)
    {
        lastPage = "/";
        this.navigationManager = navigationManager;
    }

    public void SaveCurrentPage()
    {
        lastPage = navigationManager.Uri;
    }

    public void NavigateToLastPage()
    {
        navigationManager.NavigateTo(lastPage);
    }
}
