using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Answer;
public record AnswerListModel : ModelBase
{
    public string Text { get; set; }
    public Guid QuestionId { get; set; }
    public QuestionDetailModel Question { get; set; }
    public Guid UserId { get; set; }
    public UserDetailModel User { get; set; }
    public DateTime CreationTime { get; set; }
    public List<UserDetailModel> UsersSawQuestion { get; set; } = new List<UserDetailModel>();
}
