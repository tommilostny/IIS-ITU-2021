namespace Fituska.DAL.Entities;
public class DiscussionEntity : EntityBase
{
    public string Text { get; set; }
    public Guid AuthorId { get; set; }
    public UserEntity Author { get; set; }
    public Guid OriginId { get; set; }
    public DiscussionEntity OriginDiscussion { get; set; }
    public ValueCollection<FileEntity>? Files { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid AnswerId { get; set; }
    public AnswerEntity Answer { get; set; }

    public override bool Equals(object? obj)
    {
        DiscussionEntity? discussion = obj as DiscussionEntity;
        if (GetHashCode() != obj?.GetHashCode()) return false;
        if (OriginDiscussion is not null && discussion?.OriginDiscussion is not null)
        {
            if (OriginDiscussion.Equals(discussion?.OriginDiscussion)) return false;
        }
        if (Files is not null && discussion.Files is not null)
        {
            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].Id != discussion.Files[i].Id) return false;
                if ((bool)!(Files[i].Content?.SequenceEqual(discussion.Files[i].Content))) return false;
            }
        }
        if (Answer is not null && discussion?.Answer is not null)
        {
            if ((bool)!Answer?.Equals(discussion?.Answer)) return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, AuthorId, Text, OriginId, CreationTime);
    }
}
