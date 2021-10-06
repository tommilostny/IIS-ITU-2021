using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fituska.Server.Entities
{
    public class File : EntityBase
    {
        public string Name {  get; set; }
        public byte[] Content {  get; set; }
        public Guid QuestionId {  get; set; }
        public Question Question { get; set; }
        public Guid AnswerId { get; set; }
        public Answer Answer {  get; set; }
        public Guid DiscussionId { get; set; }
        public Discussion Discussion {  get; set; }
    }
}
