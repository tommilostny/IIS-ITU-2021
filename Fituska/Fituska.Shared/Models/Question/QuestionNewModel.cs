namespace Fituska.Shared.Models.Question;

public record QuestionNewModel : ModelBase
{
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
}
