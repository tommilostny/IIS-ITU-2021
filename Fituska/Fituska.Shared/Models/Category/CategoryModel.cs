namespace Fituska.Shared.Models.Category;

public record CategoryModel : ModelBase
{
    public string Name { get; set; }
    public List<QuestionListModel> Questions { get; set; }
}
