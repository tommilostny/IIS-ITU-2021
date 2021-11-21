using Fituska.Shared.Models.Answer;
using Fituska.Shared.Models.User;
using Fituska.Shared.Models.Vote;

namespace Fituska.Shared.Models.Question;

public record QuestionDetailModel : ModelBase
{
    public string Title { get; set; }
    public string Text { get; set; }
    public UserListModel User { get; set; }
    public List<AnswerModel> Answers { get; set; } = new();
    public DateTime CreationTime { get; set; }
}
