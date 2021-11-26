using Fituska.Shared.Enums;

namespace Fituska.Shared.Models.Course;

public record CourseNewModel : ModelBase
{
    public string Name { get; set; }
    public string Shortcut { get; set; }
    public string Description { get; set; }
    public byte Credits { get; set; }
    public YearOfStudy YearOfStudy { get; set; }
    public Semester Semester { get; set; }

    public Guid LecturerId { get; set; }
}
