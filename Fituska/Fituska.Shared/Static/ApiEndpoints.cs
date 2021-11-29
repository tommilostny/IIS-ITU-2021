namespace Fituska.Shared.Static;

public static class ApiEndpoints
{
#if DEBUG
    public const string ServerBaseUrl = "https://localhost:7120/";
#else
    public const string ServerBaseUrl = "https://fituska.azurewebsites.net/";
#endif

    public const string UserBaseUrl = $"{ServerBaseUrl}api/user";
    public const string RegisterUrl = $"{UserBaseUrl}/register";
    public const string SignInUrl = $"{UserBaseUrl}/signin";

    public static string UserUrl(string userName) => $"{UserBaseUrl}/{userName}";

    public static string QuestionUrl(Guid id) => $"{ServerBaseUrl}api/question/{id}";

    public const string SearchUrl = $"{ServerBaseUrl}api/search";
}
