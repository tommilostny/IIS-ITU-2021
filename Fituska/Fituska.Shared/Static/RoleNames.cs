namespace Fituska.Shared.Static;

public static class RoleNames
{
    public const string AdminRoleName = "Administrator";
    public const string ModeratorRoleName = "Moderator";
    public const string StudentRoleName = "Student";
    public const string LecturerRoleName = "Lecturer";

    /// <summary> Iterator over available role name constants. </summary>
    public static IEnumerable<string> GetAll()
    {
        yield return StudentRoleName;
        yield return LecturerRoleName;
        yield return ModeratorRoleName;
        yield return AdminRoleName;
    }
}
