using Fituska.Shared.Enums;
using Fituska.Shared.Models.Category;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Course;

public record CourseDetailModel : ModelBase
{
    public string Name { get; set; }
    public string Shortcut { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public byte Credits { get; set; }
    public YearOfStudy YearOfStudy { get; set; }
    public Semester Semester { get; set; }

    public UserListModel Lecturer { get; set; }

    public List<CategoryDetailModel> Categories { get; set; }
}
