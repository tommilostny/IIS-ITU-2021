using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Comment;

public record CommentDetailModel : ModelBase
{
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public UserListModel User { get; set; }
    public Guid UserId { get; set; }
    public List<CommentDetailModel> SubComments { get; set; } = new();
}
