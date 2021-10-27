namespace Fituska.Shared.Models.User;

public record UserDetailModel : ModelBase
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? DiscordUsername { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public Guid? PhotoID { get; set; }

    public PhotoModel? Photo { get; set; }

    public List<CourseAttendencyModel> AttendingCourses { get; set; } = new();
}
