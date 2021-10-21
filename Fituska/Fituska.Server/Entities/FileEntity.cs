namespace Fituska.Server.Entities;

public class FileEntity : EntityBase
{
    public string Name { get; set; }
    
    public byte[] Content { get; set; }
    
    public Guid QuestionId { get; set; }
    
    public QuestionEntity Question { get; set; }
    
    public Guid AnswerId { get; set; }
    
    public AnswerEntity Answer { get; set; }
    
    public Guid DiscussionId { get; set; }
    
    public DiscussionEntity Discussion { get; set; }
}
