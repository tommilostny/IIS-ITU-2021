using Fituska.Shared.Enums;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Vote;

public record VoteModel : ModelBase
{
    public UserListModel User { get; set; }
    public QuestionVote Vote { get; set; }
}
