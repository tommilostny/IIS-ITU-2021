using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fituska.Server.Entities.Interfaces;

namespace Fituska.Server.Entities
{
    public class User : IdentityUser<Guid> , IEntity
    {
        public string Name {  get; set; }
        public string Surname {  get; set; }
        public string DiscordUserName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public Guid PhotoID { get; set; }
        public byte[] Photo { get; set; }      
        public List<UserAttendsCourse> AttendingCourses {  get; set; }
    }
}
