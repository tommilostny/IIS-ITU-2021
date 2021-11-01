namespace Fituska.DAL.Entities;
public class UserSawAnswer : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public Guid AnswerId { get; set; }
    public AnswerEntity? Answer { get; set; }

    public override bool Equals(object? courseAttendence)
    {
        if (GetHashCode() != (courseAttendence?.GetHashCode())) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, UserId, AnswerId);
    }
}
