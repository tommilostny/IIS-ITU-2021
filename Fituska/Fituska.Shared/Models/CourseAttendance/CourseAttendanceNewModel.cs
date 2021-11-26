namespace Fituska.Shared.Models.CourseAttendance;

public record CourseAttendanceNewModel : ModelBase
{
    public short AttendingYear { get; set; }
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
}
