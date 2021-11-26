namespace Fituska.Client.Shared;

public class LastPageSavableComponentBase : ComponentBase
{
    [Inject]
    private LastPageStorageService LastPageStorageService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        LastPageStorageService.SaveCurrentPage();
        await base.OnInitializedAsync();
    }
}
