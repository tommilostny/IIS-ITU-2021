using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fituska.Server.Entities.Interfaces;
using System.Diagnostics.CodeAnalysis;

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

        public override bool Equals(object obj)
        {
            if (obj is not User) return false;
            User comparingUser = obj as User;
            return Id == comparingUser.Id;
        }

        private sealed class UserEqualityComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x is null) return false;
                if (y is null) return false;
                if (x.GetType() != y.GetType()) return false;
                if(x.Id != y.Id) return false;
                if(x.Name != y.Name) return false;
                if(x.DiscordUserName != y.DiscordUserName) return false;
                if(x.Email != y.Email) return false;
                if(x.UserName != y.UserName) return false;
                if(x.RegistrationDate != y.RegistrationDate) return false;
                return true;
            }

            public int GetHashCode([DisallowNull] User obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
