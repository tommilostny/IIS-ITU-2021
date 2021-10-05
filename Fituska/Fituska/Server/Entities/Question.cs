using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fituska.Server.Entities
{
    public class Question
    {
        [Key]
        public Guid Id {  get; set; }

        public string Name {  get; set; }

        public string Text {  get; set; }

        public Guid ApplicationUserId { get; set; }

        public User ApplicationUser {  get; set; }

        public Guid CategoryId {  get; set; }

        public Category Category {  get; set; }

        public List<Answer> Answers {  get; set; }
        public DateTime CreationTime {  get; set; }

        public Guid QuestionVoteId {  get; set; }
        public List<UserVoteQuestion> UsersVoteQuestion {  get; set; }
    }
}
