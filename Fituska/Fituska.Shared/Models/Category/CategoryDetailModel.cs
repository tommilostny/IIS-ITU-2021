namespace Fituska.Shared.Models.Category;

public record CategoryDetailModel : ModelBase
{
    public string Name { get; set; }
    public int Year { get; set; }
    public Guid CourseId { get; set; }
    public List<QuestionListModel> Questions { get; set; }
}
