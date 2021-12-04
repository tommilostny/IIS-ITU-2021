namespace Fituska.DAL.Entities;

public class QuestionEntity : EntityBase
{
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }

    public ValueCollection<AnswerEntity> Answers { get; set; } = new();
    public ValueCollection<UserSawQuestionEntity> UserSawQuestions { get; set; } = new();
    public ValueCollection<FileEntity> Files { get; set; } = new();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
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
