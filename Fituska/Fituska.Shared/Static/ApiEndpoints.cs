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
    public const string PasswordChangeUrl = $"{UserBaseUrl}/passwordchange";
    public const string UserRoleChangeUrl = $"{UserBaseUrl}/rolechange";

    public const string SearchUrl = $"{ServerBaseUrl}api/search";
    public const string AnswerUrl = $"{ServerBaseUrl}api/answer";
    public const string CategoryUrl = $"{ServerBaseUrl}api/category";
    public const string CommentUrl = $"{ServerBaseUrl}api/comment";
    public const string CourseUrl = $"{ServerBaseUrl}api/course";
    public const string CourseAttendanceUrl = $"{ServerBaseUrl}api/courseattendance";
    public const string QuestionUrl = $"{ServerBaseUrl}api/question";
    public const string UserSawQuestionUrl = $"{ServerBaseUrl}api/usersawquestion";
    public const string VoteUrl = $"{ServerBaseUrl}api/vote";

    public const string FileUrl = $"{ServerBaseUrl}api/file";
    public const string UserFileUrl = $"{FileUrl}/user";
    public const string QuestionFileUrl = $"{FileUrl}/question";
    public const string AnswerFileUrl = $"{FileUrl}/answer";

    public static string UserNameUrl(string userName) => $"{UserBaseUrl}/{userName}";

    public static string AnswerIdUrl(Guid id) => $"{AnswerUrl}/{id}";

    public static string CategoryIdUrl(Guid id) => $"{CategoryUrl}/{id}";

    public static string CommentIdUrl(Guid id) => $"{CommentUrl}/{id}";

    public static string CourseIdUrl(Guid id) => $"{CourseUrl}/{id}";

    public static string CourseUrlUrl(string url) => $"{CourseUrl}/{url}";

    public static string CourseEditUrl(string url) => $"{CourseUrl}/foredit/{url}";

    public static string CourseLecrurersUrl(Guid userId) => $"{CourseUrl}/user/{userId}";

    public static string CourseModeratorUrl() => $"{CourseUrl}/forapproval";

    public static string CourseModeratorApprovalUrl(Guid id) => $"{CourseUrl}/approve/{id}";

    public static string FileIdUrl(Guid id) => $"{FileUrl}/{id}";

    public static string CourseAttendanceIdUrl(Guid id) => $"{CourseAttendanceUrl}/{id}";

    public static string CourseAttendanceUserIdUrl(Guid id) => $"{CourseAttendanceUrl}/user/{id}";

    public static string QuestionIdUrl(Guid id) => $"{QuestionUrl}/{id}";

    public static string UserIdFileUrl(Guid id) => $"{UserFileUrl}/{id}";

    public static string UserSawQuestionIdUrl(Guid id) => $"{UserSawQuestionUrl}/{id}";

    public static string VoteIdUrl(Guid id) => $"{VoteUrl}/{id}";

    public static string VoteAnswerIdUrl(Guid id) => $"{VoteUrl}/answer/{id}";

    public static string UserSawQuestionQuestionIdUrl(Guid id) => $"{UserSawQuestionUrl}/question/{id}";
}
