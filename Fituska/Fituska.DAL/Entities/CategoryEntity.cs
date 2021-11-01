namespace Fituska.DAL.Entities;

public class CategoryEntity : EntityBase
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CourseId { get; set; }

    public CourseEntity Course { get; set; }

    public override bool Equals(object? obj)
    {
        CategoryEntity? category = obj as CategoryEntity;
        if (GetHashCode() != obj?.GetHashCode()) return false;
        if (!Course.Equals(category?.Course)) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, CourseId);
    }
}
