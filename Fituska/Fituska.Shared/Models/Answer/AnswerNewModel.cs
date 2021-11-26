namespace Fituska.Shared.Models.Answer;

public record AnswerNewModel : ModelBase
{
    public string Text { get; set; }
    public Guid QuestionId { get; set; }
    public Guid UserId { get; set; }
}
