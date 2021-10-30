using Fituska.DAL.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Fituska.DAL.Entities;
public class UserSawQuestion : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public Guid QuestionId { get; set; }
    public QuestionEntity? Question { get; set; }
}
