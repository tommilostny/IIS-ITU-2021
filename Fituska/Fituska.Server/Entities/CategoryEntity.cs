namespace Fituska.Server.Entities;

public class CategoryEntity : EntityBase
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CourseId { get; set; }

    public CourseEntity Course { get; set; }
}
