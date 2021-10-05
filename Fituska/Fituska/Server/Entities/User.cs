using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Fituska.Server.Entities
{
    public class User : IdentityUser
    {
        public string DiscordUserName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string PhotoID { get; set; }
        public List<Answer> WrittenAnswers {  get; set; }
        public List<UserAttendsCourse> AttendingCourses {  get; set; }
    }
}
