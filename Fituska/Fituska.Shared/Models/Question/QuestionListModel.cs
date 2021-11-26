using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.Question;

public record QuestionListModel : ModelBase
{
    public string Title { get; set; }
    public UserListModel User { get; set; }
    public DateTime CreationTime { get; set; }
}
