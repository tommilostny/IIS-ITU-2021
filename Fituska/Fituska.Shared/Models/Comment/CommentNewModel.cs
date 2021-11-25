namespace Fituska.Shared.Models.Comment;

public record CommentNewModel : ModelBase
{
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid UserId { get; set; }
    public Guid? AnswerId { get; set; }
    public Guid? ParentCommentId { get; set; }
}
