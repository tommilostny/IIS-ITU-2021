namespace Fituska.Shared.Models.Category;

public record CategoryNewModel : ModelBase
{
    public string Name { get; set; }
    public int Year { get; set; }
    public Guid CourseId { get; set; }
}
