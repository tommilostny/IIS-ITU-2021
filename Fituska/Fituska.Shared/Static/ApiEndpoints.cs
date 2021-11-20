namespace Fituska.Shared.Static;

public static class ApiEndpoints
{
#if DEBUG
    public const string ServerBaseUrl = "https://localhost:7120/";
#else
    public const string ServerBaseUrl = "https://fituska.net/";
#endif

    public const string RegisterUrl = $"{ServerBaseUrl}api/user/register";
    public const string SignInUrl = $"{ServerBaseUrl}api/user/signin";
    public const string WeatherForecastUrl = $"{ServerBaseUrl}weatherforecast";

    public static string UserGetUrl(string userId) => $"{ServerBaseUrl}api/user/{userId}";

    public const string ImageUploadUrl = $"{ServerBaseUrl}api/imageupload";
}
