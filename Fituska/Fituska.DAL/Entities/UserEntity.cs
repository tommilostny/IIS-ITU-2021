using Fituska.DAL.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Fituska.DAL.Entities;
public class UserEntity : IdentityUser<Guid>, IEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? DiscordUsername { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public Guid? PhotoID { get; set; }

    public PhotoEntity? Photo { get; set; }

    public ValueCollection<CourseAttendanceEntity> AttendingCourses { get; set; } = new();

    public override bool Equals(object? comparingObject)
    {
        if (comparingObject is not UserEntity userEntity) return false;
        if (GetHashCode() != userEntity.GetHashCode()) return false;
        if (DiscordUsername != userEntity.DiscordUsername) return false;
        if (RegistrationDate != userEntity.RegistrationDate) return false;
        if (!AttendingCourses.SequenceEqual(userEntity.AttendingCourses)) return false;
        if (LastLoginDate != userEntity.LastLoginDate) return false;
        if (Email != userEntity.Email) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, PasswordHash);
    }
}
