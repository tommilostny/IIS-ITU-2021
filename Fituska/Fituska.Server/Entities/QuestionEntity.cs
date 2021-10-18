namespace Fituska.Server.Entities;

public class QuestionEntity : EntityBase
{
    public string Title { get; set; }
    
    public string Text { get; set; }
    
    public Guid UserId { get; set; }
    
    public UserEntity User { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public CategoryEntity Category { get; set; }
    
    public List<AnswerEntity> Answers { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public Guid QuestionVoteId { get; set; }
    
    public List<VoteEntity> UsersVoteQuestion { get; set; }
}
