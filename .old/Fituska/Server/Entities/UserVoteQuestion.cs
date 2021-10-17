using System;
using System.Collections.Generic;
using System.Linq;
using Fituska.Server.Enums;

namespace Fituska.Server.Entities
{
    public class UserVoteQuestion: EntityBase
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public QuestionVote Vote { get; set; }
    }
}
