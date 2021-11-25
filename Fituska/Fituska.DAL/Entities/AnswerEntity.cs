namespace Fituska.DAL.Entities;

public class AnswerEntity : EntityBase
{
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    
    public Guid QuestionId { get; set; }
    public QuestionEntity Question { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    
    public ValueCollection<UserSawAnswerEntity> UsersSawAnswer { get; set; } = new();
    public ValueCollection<VoteEntity> UsersVoteAnswers { get; set; } = new();
    public ValueCollection<CommentEntity> Comments { get; set; } = new();
    public ValueCollection<FileEntity> Files { get; set; } = new();
    
    public override bool Equals(object? entity)
    {
        var answer = (AnswerEntity?)entity;
        if (GetHashCode() != answer?.GetHashCode()) return false;
        if (!UsersSawAnswer.SequenceEqual(answer.UsersSawAnswer)) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, QuestionId, UserId, CreationTime);
    }
}
