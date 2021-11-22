namespace Fituska.Client.Shared;

public class LastPageSavableComponentBase : ComponentBase
{
    [Inject]
    private LastPageStorageProvider LastPageStorageProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        LastPageStorageProvider.SaveCurrentPage();
        await base.OnInitializedAsync();
    }
}
