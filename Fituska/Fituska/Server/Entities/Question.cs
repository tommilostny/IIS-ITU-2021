using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fituska.Server.Entities
{
    public class Question : EntityBase
    {
        public string Name {  get; set; }
        public string Text {  get; set; }
        public Guid UserId { get; set; }
        public User User {  get; set; }
        public Guid CategoryId {  get; set; }
        public Category Category {  get; set; }
        public List<Answer> Answers {  get; set; }
        public DateTime CreationTime {  get; set; }
        public Guid QuestionVoteId {  get; set; }
        public List<UserVoteQuestion> UsersVoteQuestion {  get; set; }
    }
}
