namespace Fituska.DAL.Entities;

public class UserSawAnswerEntity : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid? AnswerId { get; set; }
    public AnswerEntity Answer { get; set; }

    public override bool Equals(object? entity)
    {
        if (GetHashCode() != (entity?.GetHashCode())) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, UserId, AnswerId);
    }
}
