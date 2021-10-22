namespace Fituska.Server.Entities;

public class CourseAttendanceEntity : EntityBase
{
    public short AttendingYear { get; set; }

    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public Guid CourseId { get; set; }

    public CourseEntity Course { get; set; }

    public override bool Equals(object? courseAttendence)
    {
        return GetHashCode() == courseAttendence?.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, AttendingYear, UserId, CourseId);
    }
}
