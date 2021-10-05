using System;

namespace Fituska.Server.Entities
{
    public class QuestionVotes
    {
        public Guid Id {  get; set; }

        public Vote Vote {  get; set; }

        public Guid ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser {  get; set; }

        public Guid QuestionId {  get; set; }

        public Question Question {  get; set; }
    }

    public enum Vote
    {
        Dislike, Like
    }
}
