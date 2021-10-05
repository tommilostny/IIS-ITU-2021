using System;
using System.Collections.Generic;

namespace Fituska.Server.Entities
{
    public class Question
    {
        public Guid Id {  get; set; }

        public string Name {  get; set; }

        public string Text {  get; set; }

        public Guid ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser {  get; set; }

        public Guid CategoryId {  get; set; }

        public Category Category {  get; set; }

        public List<Answer> Answers {  get; set; }
    }
}
