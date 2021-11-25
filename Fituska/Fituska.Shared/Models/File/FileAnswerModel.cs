namespace Fituska.Shared.Models.File;

public record FileAnswerModel : FileModelBase
{
    public Guid AnswerId { get; set; }
}
