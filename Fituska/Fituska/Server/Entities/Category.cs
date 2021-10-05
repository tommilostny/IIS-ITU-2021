using System;

namespace Fituska.Server.Entities
{
    public class Category
    {
        public Guid Id {  get; set; }

        public string Name {  get; set; }

        public string Description {  get; set; }

        public Guid CourseId {  get; set; }

        public Course Course {  get; set; }
    }
}
