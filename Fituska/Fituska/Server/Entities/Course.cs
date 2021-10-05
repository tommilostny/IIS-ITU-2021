using System;

namespace Fituska.Server.Entities
{
    public class Course
    {
        public Guid Id {  get; set; }

        public short AcademicYear {  get; set; }

        public string Name {  get; set; }

        public string Shortcut {  get; set; }

        public string Description {  get; set; }
    
        public string Url {  get; set; }

        public byte Credits {  get; set; }

        public YearOfStudy YearOfStudy {  get; set; }

        public byte Semester {  get; set; }
    }

    public enum YearOfStudy
    {
        BIT1 = 1, BIT2, BIT3, MIT1, MIT2 
    }
}
