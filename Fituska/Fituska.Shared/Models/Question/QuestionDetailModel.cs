using Fituska.Shared.Models.Answer;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Question;

public record QuestionDetailModel : ModelBase
{
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public UserListModel User { get; set; }
    public List<AnswerDetailModel> Answers { get; set; } = new();
    public List<UserSawQuestionModel> UserSawQuestions { get; set; } = new();
}
