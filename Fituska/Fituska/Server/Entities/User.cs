using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fituska.Server.Entities
{
    public class User : IdentityUser
    {
        public string DiscordUserName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string PhotoID { get; set; }
        public List<Answer> Answers {  get; set; } // This invoke error
        public List<UserAttendsCourse> AttendingCourses {  get; set; }
    }
}
