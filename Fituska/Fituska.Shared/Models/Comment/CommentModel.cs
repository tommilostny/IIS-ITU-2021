using Fituska.Shared.Models.File;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Comment;

public record CommentModel : ModelBase
{
    public string Text { get; set; }
    public UserListModel User { get; set; }
    public DateTime CreationTime { get; set; }
    public List<CommentModel> Comments { get; set; } = new();
}
