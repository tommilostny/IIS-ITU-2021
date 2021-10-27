using Fituska.Shared.Enums;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Vote;
public record VoteListModel : ModelBase
{
    public Guid UserId { get; set; }
    public UserDetailModel User { get; set; }
    public Guid QuestionId { get; set; }
    public QuestionDetailModel Question { get; set; }
    public QuestionVote Vote { get; set; }
}
