namespace Fituska.DAL.Entities;
public class QuestionEntity : EntityBase
{
    public string Title { get; set; }

    public string Text { get; set; }

    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public Guid CategoryId { get; set; }

    public CategoryEntity Category { get; set; }

    public ValueCollection<AnswerEntity> Answers { get; set; } = new ValueCollection<AnswerEntity>();

    public DateTime CreationTime { get; set; }

    public ValueCollection<VoteEntity> UsersVoteQuestion { get; set; } = new ValueCollection<VoteEntity>();

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is not QuestionEntity question) return false;
        if (GetHashCode() != obj?.GetHashCode()) return false;
        if (CreationTime != question.CreationTime) return false;
        if (CategoryId != question.CategoryId) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, Text, UserId);
    }
}
