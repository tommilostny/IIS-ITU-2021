using Fituska.Shared.Models.Comment;
using Fituska.Shared.Models.File;
using Fituska.Shared.Models.User;
using Fituska.Shared.Models.Vote;

namespace Fituska.Shared.Models.Answer;

public record AnswerDetailModel : ModelBase
{
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid QuestionId { get; set; }
    public UserListModel User { get; set; }
    public List<UserSawAnswerModel> UsersSawAnswer { get; set; }
    public List<VoteModel> Votes { get; set; } = new();
    public List<CommentDetailModel> Comments { get; set; } = new();
    public List<FileModelBase> Files { get; set; } = new();
}
