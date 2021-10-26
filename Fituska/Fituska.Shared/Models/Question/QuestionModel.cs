namespace Fituska.Shared.Models;

//TODO: list/detail model??
public class QuestionModel
{
    public string Title { get; set; }

    public string Text { get; set; }

    public Guid UserId { get; set; }

    //public UserEntity User { get; set; }

    public Guid CategoryId { get; set; }

    //public CategoryEntity Category { get; set; }

    //public ValueCollection<AnswerEntity> Answers { get; set; } = new ValueCollection<AnswerEntity>();

    public DateTime CreationTime { get; set; }

    //public ValueCollection<VoteEntity> UsersVoteQuestion { get; set; } = new ValueCollection<VoteEntity>();
}
