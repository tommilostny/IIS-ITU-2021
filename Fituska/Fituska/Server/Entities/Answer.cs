using System;
using System.Collections.Generic;
using AutoMapper;

namespace Fituska.Server.Entities
{
    public class Answer : EntityBase
    {
        public string Text {  get; set; }
        public Guid QuestionId {  get; set; }
        public Question Question {  get; set; }
        public Guid UserId {  get; set; }
        public User User {  get; set; }
        public DateTime CreationTime {  get; set; }
        public List<User> UsersSawQuestion {  get; set; }
    }

    public class AnswerEntityMapperProfile : Profile
    {
        public AnswerEntityMapperProfile()
        {
            CreateMap<Answer, Answer>();
        }
    }
}
