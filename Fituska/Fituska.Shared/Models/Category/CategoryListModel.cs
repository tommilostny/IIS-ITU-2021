using Fituska.Shared.Models.Course;

namespace Fituska.Shared.Models.Category;
public record CategoryListModel : ModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CourseId { get; set; }
    public CourseDetailModel Course { get; set; }
}
