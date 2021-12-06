namespace Fituska.Shared.Models.Course;

public record CourseEditModel : CourseNewModel
{
    public bool ModeratorApproved { get; set; }
}
