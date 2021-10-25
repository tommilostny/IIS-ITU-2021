namespace Fituska.DAL.Entities;

public class CategoryEntity : EntityBase
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CourseId { get; set; }

    public CourseEntity Course { get; set; }

    public override bool Equals(object? category)
    {
        return GetHashCode() == category?.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, CourseId);
    }
}
