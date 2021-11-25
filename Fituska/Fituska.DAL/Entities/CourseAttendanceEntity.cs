namespace Fituska.DAL.Entities;

public class CourseAttendanceEntity : EntityBase
{
    public short AttendingYear { get; set; }
   
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid CourseId { get; set; }
    public CourseEntity Course { get; set; }

    public override bool Equals(object? courseAttendence)
    {
        if (GetHashCode() != (courseAttendence?.GetHashCode())) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, AttendingYear, UserId, CourseId);
    }
}
