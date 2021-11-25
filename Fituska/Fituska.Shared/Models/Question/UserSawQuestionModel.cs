namespace Fituska.Shared.Models.Question;

public record UserSawQuestionModel : ModelBase
{
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
}
