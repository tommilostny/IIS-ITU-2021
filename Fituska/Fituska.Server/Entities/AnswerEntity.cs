﻿//using AutoMapper;

namespace Fituska.Server.Entities;

public class AnswerEntity : EntityBase
{
    public string Text { get; set; }
    
    public Guid QuestionId { get; set; }
    
    public QuestionEntity Question { get; set; }
    
    public Guid UserId { get; set; }
    
    public UserEntity User { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public List<UserEntity> UsersSawQuestion { get; set; }
}

//public class AnswerEntityMapperProfile : Profile
//{
//    public AnswerEntityMapperProfile()
//    {
//        CreateMap<Answer, Answer>();
//    }
//}
