using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fituska.Server.Entities
{
    public class Discussion : EntityBase
    {
        public string Text {  get; set; }
        public Guid AuthorId {  get; set; }
        public User Author {  get; set; }
        public Guid OriginId {  get; set; }
        public Discussion OriginDiscussion {  get; set;}
        public List<File> File {  get; set; }
        public DateTime CreationTime {  get; set; }
        public Guid AnswerId {  get; set; }
        public Answer Answer {  get; set; }
    }
}
