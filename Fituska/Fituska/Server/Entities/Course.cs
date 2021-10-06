using System;
using Fituska.Server.Enums;

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
    }
}
