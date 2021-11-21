using Fituska.Shared.Models.Comment;
using Fituska.Shared.Models.User;
using Fituska.Shared.Models.Vote;

namespace Fituska.Shared.Models.Answer;

public record AnswerModel : ModelBase
{
    public string Text { get; set; }
    public UserListModel User { get; set; }
    public DateTime CreationTime { get; set; }
    public List<VoteModel> Votes { get; set; } = new();
    public List<CommentModel> Comments { get; set; } = new();
}
