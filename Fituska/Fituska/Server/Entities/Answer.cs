using System;
using System.Collections.Generic;

namespace Fituska.Server.Entities
{
    public class Answer
    {
        public Guid Id {  get; set; }

        public string Text {  get; set; }

        public Guid QuestionId {  get; set; }

        public Question Question {  get; set; }

        public Guid UserId {  get; set; }

        public User User {  get; set; }
        public DateTime CreationTime {  get; set; }
        public List<User> UsersSawQuestion {  get; set; }
    }
}
