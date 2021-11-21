using Fituska.Shared.Models.Answer;
using Fituska.Shared.Models.Comment;

namespace Fituska.Shared.Models.File;

public record FileDetailModel : ModelBase
{
    public string Name { get; set; }
    public byte[]? Content { get; set; }
    public Guid? QuestionId { get; set; }
    public QuestionDetailModel Question { get; set; }
    public Guid? AnswerId { get; set; }
    public AnswerModel? Answer { get; set; }
    public Guid? DiscussionId { get; set; }
    public CommentModel? Discussion { get; set; }
}
