namespace Fituska.Client.Services;

public class LastPageStorageService
{
    private string lastPage;
    private readonly NavigationManager navigationManager;

    public LastPageStorageService(NavigationManager navigationManager)
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
