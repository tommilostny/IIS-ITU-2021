using Fituska.Shared.Models.Answer;
using Fituska.Shared.Models.File;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Discussion;
public record DiscussionDetailModel : ModelBase
{
    public string Text { get; set; }
    public Guid AuthorId { get; set; }
    public UserDetailModel Author { get; set; }
    public Guid OriginId { get; set; }
    public DiscussionDetailModel OriginDiscussion { get; set; }
    public List<FileDetailModel> Files { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid AnswerId { get; set; }
    public AnswerDetailModel Answer { get; set; }
}
