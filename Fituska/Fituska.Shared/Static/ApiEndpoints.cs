namespace Fituska.Shared.Static;

public static class ApiEndpoints
{
#if DEBUG
    public const string _serverBaseUrl = "https://localhost:7120/";
#else
    public const string _serverBaseUrl = "https://fituska.net/";
#endif

    public const string _registerUrl = $"{_serverBaseUrl}api/user/register";
    public const string _signInUrl = $"{_serverBaseUrl}api/user/signin";
    public const string _weatherForecastUrl = $"{_serverBaseUrl}weatherforecast";
}
