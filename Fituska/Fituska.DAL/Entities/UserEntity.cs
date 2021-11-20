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
    public string? PhotoUrl { get; set; }

    public ValueCollection<CourseAttendanceEntity> AttendingCourses { get; set; } = new();

    public override bool Equals(object? comparingObject)
    {
        UserEntity user = (UserEntity)comparingObject!;
        if (user is null) return false;
        if (GetHashCode() != user.GetHashCode()) return false;
        if (DiscordUsername != user.DiscordUsername) return false;
        if (RegistrationDate != user.RegistrationDate) return false;
        if (!AttendingCourses.SequenceEqual(user.AttendingCourses)) return false;
        if (PhotoUrl != user.PhotoUrl) return false;
        if (LastLoginDate != user.LastLoginDate) return false;
        if (Email != user.Email) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, PasswordHash);
    }
}
