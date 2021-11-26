namespace Fituska.Shared.Models.File;

public record FileQuestionModel : FileModelBase
{
    public Guid QuestionId { get; set; }
}
