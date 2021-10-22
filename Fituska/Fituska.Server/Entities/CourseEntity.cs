namespace Fituska.Server.Entities;

public class CourseEntity : EntityBase
{
    public short AcademicYear { get; set; }
    
    public string Name { get; set; }
    
    public string Shortcut { get; set; }
    
    public string Description { get; set; }
    
    public string Url { get; set; }
    
    public byte Credits { get; set; }
    
    public YearOfStudy YearOfStudy { get; set; }
    
    public Semester Semester { get; set; }
    
    public List<CourseAttendanceEntity> Users { get; set; }

    public override bool Equals(object? obj)
    {
        if(GetHashCode() != obj?.GetHashCode()) return false;
        CourseEntity? course = obj as CourseEntity;
        if (Description != course?.Description) return false;
        if (Url != course?.Url) return false;
        if( YearOfStudy != course?.YearOfStudy) return false;
        if (Credits != course.Credits) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, AcademicYear, Name);
    }

}
