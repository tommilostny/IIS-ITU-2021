using System;

namespace Fituska.Server.Entities
{
    public class Answer
    {
        public Guid Id {  get; set; }

        public string Text {  get; set; }

        public Guid QuestionId {  get; set; }

        public Question Question {  get; set; }

        public Guid ApplicationUserId {  get; set; }

        public ApplicationUser ApplicationUser {  get; set; }
    }
}
