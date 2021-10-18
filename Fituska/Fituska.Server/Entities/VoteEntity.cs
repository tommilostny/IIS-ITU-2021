using Fituska.Server.Enums;

namespace Fituska.Server.Entities;

public class VoteEntity : EntityBase
{
    public Guid UserId { get; set; }
    
    public UserEntity User { get; set; }
    
    public Guid QuestionId { get; set; }
    
    public QuestionEntity Question { get; set; }
    
    public QuestionVote Vote { get; set; }
}
