using Fituska.Shared.Enums;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Vote;

public record VoteNewModel : ModelBase
{
    public VoteValue Vote { get; set; }
    public Guid UserId { get; set; }
    public Guid AnswerId { get; set; }
}
