namespace Fituska.Server.Entities;

public class FileEntity : EntityBase
{
    public string Name { get; set; }
    
    public byte[]? Content { get; set; }
    
    public Guid? QuestionId { get; set; }
    
    public QuestionEntity? Question { get; set; }
    
    public Guid? AnswerId { get; set; }
    
    public AnswerEntity? Answer { get; set; }
    
    public Guid? DiscussionId { get; set; }
    
    public DiscussionEntity? Discussion { get; set; }

    public override bool Equals(object? obj)
    {
        if (GetHashCode() != obj?.GetHashCode()) return false;
        FileEntity? file = (FileEntity?)obj;
        if (Content == null && file?.Content == null) return true;
        bool? same = Content?.SequenceEqual(file?.Content!);
        if (same is null) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id,Name);
    }
}
