using Fituska.Shared.Enums;

namespace Fituska.Shared.Models.Course;

public record CourseListModel : ModelBase
{
    public string Shortcut { get; set; }
    public string Url { get; set; }
    public bool ModeratorApproved { get; set; }
    public YearOfStudy YearOfStudy { get; set; }
    public Semester Semester { get; set; }
}
