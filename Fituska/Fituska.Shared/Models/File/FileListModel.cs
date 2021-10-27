using Fituska.Shared.Models.Answer;
using Fituska.Shared.Models.Discussion;

namespace Fituska.Shared.Models.File;
public record FileListModel : ModelBase
{
    public string Name { get; set; }
    public byte[]? Content { get; set; }
    public Guid? QuestionId { get; set; }
    public QuestionDetailModel Question { get; set; }
    public Guid? AnswerId { get; set; }
    public AnswerDetailModel? Answer { get; set; }
    public Guid? DiscussionId { get; set; }
    public DiscussionDetailModel? Discussion { get; set; }
}
