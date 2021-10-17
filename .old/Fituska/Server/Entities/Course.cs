using System;
using Fituska.Server.Enums;
using System.Collections.Generic;

namespace Fituska.Server.Entities
{
    public class Course : EntityBase
    {
        public short AcademicYear {  get; set; }
        public string Name {  get; set; }
        public string Shortcut {  get; set; }
        public string Description {  get; set; }
        public string Url {  get; set; }
        public byte Credits {  get; set; }
        public YearOfStudy YearOfStudy {  get; set; }
        public Semester Semester {  get; set; }
        public List<User> Users { get; set; }
    }
}
