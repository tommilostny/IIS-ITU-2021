namespace Fituska.Shared.Models.Answer;

public record UserSawAnswerModel : ModelBase
{
    public Guid UserId { get; set; }
    public Guid AnswerId { get; set; }
}
