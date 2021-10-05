using System;

namespace Fituska.Server.Entities
{
    public class UserAttendsCourse
    {
        public Guid Id {  get; set; }

        public short AttendingYear {  get; set; }

        public Guid ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser {  get; set; }

        public Guid CourseId {  get; set; }

        public Course Course {  get; set; }
    }
}
