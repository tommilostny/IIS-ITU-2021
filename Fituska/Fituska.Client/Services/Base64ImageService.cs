namespace Fituska.Client.Services;

public class Base64ImageService
{
    private readonly string[] _extensions = { "jpg", "png", "jpeg", "gif", "svg", "bmp" };

    public string ImageFileToBase64(string fileName, byte[] data)
    {
        return $"data:{DecodeImageTypeFromFilePath(fileName)};base64,{Convert.ToBase64String(data)}";
    }

    private string DecodeImageTypeFromFilePath(string fileName)
    {
        var imgType = _extensions.FirstOrDefault(ext => fileName.EndsWith($".{ext}"));
        if (imgType is not null)
        {
            imgType = $"image/{imgType}";
        }
        return imgType;
    }
}
