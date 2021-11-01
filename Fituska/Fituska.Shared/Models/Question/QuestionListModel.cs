using Fituska.Shared.Models.Answer;
using Fituska.Shared.Models.Category;
using Fituska.Shared.Models.User;
using Fituska.Shared.Models.Vote;

namespace Fituska.Shared.Models.Question;
public record QuestionListModel : ModelBase
{
    public string? Title { get; set; }
    public string? Text { get; set; }
    public Guid UserId { get; set; }
    public UserDetailModel User { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryDetailModel Category { get; set; }
    public List<AnswerDetailModel> Answers { get; set; } = new List<AnswerDetailModel>();
    public DateTime CreationTime { get; set; }
    public List<VoteDetailModel> UsersVoteQuestion { get; set; } = new List<VoteDetailModel>();
}
