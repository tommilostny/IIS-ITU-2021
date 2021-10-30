using Fituska.DAL.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Fituska.DAL.Entities;
public class UserSawAnswer : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public Guid AnswerId { get; set; }
    public AnswerEntity? Answer { get; set; }
}
