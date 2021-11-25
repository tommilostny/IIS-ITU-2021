using Microsoft.AspNetCore.Components.Forms;

namespace Fituska.Client.Core;

public partial class FileUploader : ComponentBase
{
    //[Parameter]
    //public bool Multiple { get; set; }
    //
    //[Parameter]
    //public bool ImageOnly { get; set; }

    [Parameter]
    public EventCallback<string> OnChange { get; set; }

    [Inject]
    private HttpClient Http { get; set; }

    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        if (!e.File.ContentType.StartsWith("image/"))
        {
            return;
        }
        using var content = new MultipartFormDataContent();

        var imageFile = await e.File.RequestImageFileAsync(e.File.ContentType, 512, 512);
        var fileContent = new StreamContent(imageFile.OpenReadStream(imageFile.Size));

        //var stream = imageFile.OpenReadStream(imageFile.Size);
        //byte[] buffer;
        //await stream.ReadAsync(buffer);

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
        content.Add(fileContent, "image", imageFile.Name);

        var response = await Http.PostAsync(ApiEndpoints.ImageUploadUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var imageUrl = await response.Content.ReadAsStringAsync();
            await OnChange.InvokeAsync(imageUrl);
        }
    }
}

//https://jamessdixon.com/2013/10/01/handling-images-in-webapi/
//https://stackoverflow.com/questions/19005991/how-to-handle-images-using-webapi
//https://code-maze.com/blazor-webassembly-file-upload/
//https://docs.microsoft.com/cs-cz/aspnet/core/blazor/file-uploads?view=aspnetcore-6.0&pivots=webassembly