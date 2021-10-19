namespace Fituska.Shared.Static;

public static class RoleNames
{
    public const string _adminRoleName = "Administrator";
    public const string _moderatorRoleName = "Moderator";
    public const string _studentRoleName = "Student";
    public const string _lecturerRoleName = "Lecturer";

    /// <summary> Iterator over available role name constants. </summary>
    public static IEnumerable<string> GetAll()
    {
        yield return _studentRoleName;
        yield return _lecturerRoleName;
        yield return _moderatorRoleName;
        yield return _adminRoleName;
    }
}
