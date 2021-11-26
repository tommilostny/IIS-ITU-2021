namespace Fituska.Shared.Models.File;

public record FileCommentModel : FileModelBase
{
    public Guid CommentId { get; set; }
}
