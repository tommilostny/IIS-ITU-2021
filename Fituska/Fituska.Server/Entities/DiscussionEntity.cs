namespace Fituska.Server.Entities;

public class DiscussionEntity : EntityBase
{
    public string Text { get; set; }

    public Guid AuthorId { get; set; }

    public UserEntity Author { get; set; }

    public Guid OriginId { get; set; }

    public DiscussionEntity OriginDiscussion { get; set; }

    public List<FileEntity> Files { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid AnswerId { get; set; }

    public AnswerEntity Answer { get; set; }
}
