namespace Fituska.Shared.Models;

public record UserDetailModel : ModelBase
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? DiscordUsername { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public Guid? PhotoID { get; set; }

    //public PhotoEntity? Photo { get; set; }

    //public ValueCollection<CourseAttendanceEntity> AttendingCourses { get; set; } = new();
}
