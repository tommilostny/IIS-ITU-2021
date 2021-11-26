namespace Fituska.DAL.Entities;

public class CourseEntity : EntityBase
{
    public string Name { get; set; }
    public string Shortcut { get; set; }
    public string? Description { get; set; }
    public string Url { get; set; }
    public byte Credits { get; set; }
    public bool ModeratorApproved { get; set; }
    public YearOfStudy YearOfStudy { get; set; }
    public Semester Semester { get; set; }

    public Guid LecturerId { get; set; }
    public UserEntity Lecturer { get; set; }

    public ValueCollection<CategoryEntity> Categories { get; set; } = new();
    public ValueCollection<CourseAttendanceEntity> Attendees { get; set; } = new();

    public override bool Equals(object? obj)
    {
        CourseEntity? course = obj as CourseEntity;
        if (GetHashCode() != course?.GetHashCode()) return false;
        if (Description != course?.Description) return false;
        if (Url != course?.Url) return false;
        if (YearOfStudy != course?.YearOfStudy) return false;
        if (Credits != course.Credits) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Shortcut, Name, Url, Credits);
    }
}
