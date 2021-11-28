namespace Fituska.Shared.Models.User;

public record UserDetailModel : ModelBase
{
    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string? DiscordUsername { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? PhotoFileName { get; set; }
    public byte[]? Photo { get; set; }
    public List<CourseAttendanceListModel> AttendingCourses { get; set; }
}
