﻿namespace Fituska.DAL.Entities;
public class AnswerEntity : EntityBase
{
    public string Text { get; set; }

    public Guid QuestionId { get; set; }

    public QuestionEntity Question { get; set; }

    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public DateTime CreationTime { get; set; }

    public ValueCollection<UserEntity> UsersSawQuestion { get; set; } = new ValueCollection<UserEntity>();

    public override bool Equals(object? answerObject)
    {
        return GetHashCode() == answerObject?.GetHashCode();
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