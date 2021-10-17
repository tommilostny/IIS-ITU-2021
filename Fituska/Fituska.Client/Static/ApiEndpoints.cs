namespace Fituska.Client.Static;

internal static class ApiEndpoints
{
#if DEBUG
    internal const string _serverBaseUrl = "https://localhost:7120/";
#else
    internal const string _serverBaseUrl = "https://fituska.net/";
#endif

    internal const string _registerUrl = $"{_serverBaseUrl}api/user/register";
    internal const string _signInUrl = $"{_serverBaseUrl}api/user/signin";
    internal const string _weatherForecastUrl = $"{_serverBaseUrl}weatherforecast";
}
