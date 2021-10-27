

namespace Fituska.DAL.Entities;

public class VoteEntity : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid QuestionId { get; set; }
    public QuestionEntity Question { get; set; }
    public QuestionVote Vote { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (GetHashCode() != obj.GetHashCode()) return false;
        VoteEntity? vote = (VoteEntity?)obj;
        if (Vote != vote?.Vote) return false;
        if (QuestionId != vote?.QuestionId) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, UserId);
    }
}
