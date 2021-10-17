using System;

namespace Fituska.Server.Entities
{
    public class UserAttendsCourse : EntityBase
    {
        public short AttendingYear {  get; set; }

        public Guid UserId { get; set; }

        public User User {  get; set; }

        public Guid CourseId {  get; set; }

        public Course Course {  get; set; }
    }
}
