namespace Fituska.Shared.Static;

public static class RoleNames
{
    public const string AdminRoleName = "Administrator";
    public const string ModeratorRoleName = "Moderator";

    /// <summary> Iterator over available role name constants. </summary>
    public static IEnumerable<string> GetAll()
    {
        yield return AdminRoleName;
        yield return ModeratorRoleName;
    }
}
