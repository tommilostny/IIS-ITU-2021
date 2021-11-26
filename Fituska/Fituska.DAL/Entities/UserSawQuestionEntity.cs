namespace Fituska.DAL.Entities;

public class UserSawQuestionEntity : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid QuestionId { get; set; }
    public QuestionEntity Question { get; set; }

    public override bool Equals(object? courseAttendence)
    {
        if (GetHashCode() != (courseAttendence?.GetHashCode())) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, UserId, QuestionId);
    }
}
