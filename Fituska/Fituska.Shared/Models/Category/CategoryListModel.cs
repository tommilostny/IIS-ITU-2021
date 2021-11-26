namespace Fituska.Shared.Models.Category;

public record CategoryListModel : ModelBase
{
    public string Name { get; set; }
    public int Year { get; set; }
}
