using System;
using System.Collections.Generic;
using System.Linq;
using Fituska.Server.Enums;

namespace Fituska.Server.Entities
{
    public class UserVoteQuestion
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public User ApplicationUser { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }
        public QuestionVote Vote { get; set; }
    }
}
