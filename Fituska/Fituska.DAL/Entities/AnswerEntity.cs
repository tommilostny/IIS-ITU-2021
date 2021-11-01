namespace Fituska.DAL.Entities;
public class AnswerEntity : EntityBase
{
    public string Text { get; set; }
    public Guid QuestionId { get; set; }
    public QuestionEntity Question { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public DateTime CreationTime { get; set; }
    public ValueCollection<UserSawAnswer> UsersSawAnswer { get; set; } = new ValueCollection<UserSawAnswer>();

    public override bool Equals(object? answerObject)
    {
        var answer = (AnswerEntity?)answerObject;
        if (GetHashCode() != answer?.GetHashCode()) return false;
        if (!UsersSawAnswer.SequenceEqual(answer.UsersSawAnswer)) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, QuestionId, UserId, CreationTime);
    }
}

//public class AnswerEntityMapperProfile : Profile
//{
//    public AnswerEntityMapperProfile()
//    {
//        CreateMap<Answer, Answer>();
//    }
//}
