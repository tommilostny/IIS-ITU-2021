using Fituska.Server.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

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

    public override bool Equals(object? comparingObject)
    {
        if (comparingObject is not UserEntity userEntity) return false;
        if (GetHashCode() != userEntity.GetHashCode()) return false;
        if(DiscordUsername != userEntity.DiscordUsername) return false;
        if(RegistrationDate != userEntity.RegistrationDate) return false;
        if(AttendingCourses != userEntity.AttendingCourses) return false;
        if(LastLoginDate != userEntity.LastLoginDate) return false;
        if(Email != userEntity.Email) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, PasswordHash);
    }
}
