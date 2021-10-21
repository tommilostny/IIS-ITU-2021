﻿using Fituska.Server.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Fituska.Server.Entities;

public class UserEntity : IdentityUser<Guid>, IEntity
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? DiscordUsername { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public DateTime? LastLoginDate { get; set; }
    
    public Guid? PhotoID { get; set; }

    public PhotoEntity? Photo { get; set; }

    public List<CourseAttendanceEntity> AttendingCourses { get; set; }

    //public override bool Equals(object obj)
    //{
    //    if (obj is not User) return false;
    //    User comparingUser = obj as User;
    //    return Id == comparingUser.Id;
    //}
    //
    //private sealed class UserEqualityComparer : IEqualityComparer<User>
    //{
    //    public bool Equals(User x, User y)
    //    {
    //        if (ReferenceEquals(x, y)) return true;
    //        if (x is null) return false;
    //        if (y is null) return false;
    //        if (x.GetType() != y.GetType()) return false;
    //        if (x.Id != y.Id) return false;
    //        if (x.Name != y.Name) return false;
    //        if (x.DiscordUserName != y.DiscordUserName) return false;
    //        if (x.Email != y.Email) return false;
    //        if (x.UserName != y.UserName) return false;
    //        if (x.RegistrationDate != y.RegistrationDate) return false;
    //        return true;
    //    }
    //
    //    public int GetHashCode([DisallowNull] User obj)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}