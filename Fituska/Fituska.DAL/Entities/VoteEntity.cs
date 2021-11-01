namespace Fituska.DAL.Entities;

public class VoteEntity : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid AnswerId { get; set; }
    public AnswerEntity Answer { get; set; }
    public QuestionVote Vote { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        VoteEntity? vote = obj as VoteEntity;
        if (GetHashCode() != vote?.GetHashCode()) return false;
        if (Vote != vote?.Vote) return false;
        if (AnswerId != vote?.AnswerId) return false;
        if ((User is not null && vote?.User is null) || (User is null && vote?.User is not null)) return false;
        if (User is not null)
        {
            if (!User.Equals(vote?.User)) return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, UserId);
    }
}
