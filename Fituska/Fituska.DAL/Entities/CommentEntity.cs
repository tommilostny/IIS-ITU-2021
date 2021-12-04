namespace Fituska.DAL.Entities;

public class CommentEntity : EntityBase
{
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModifiedTime { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid? AnswerId { get; set; }
    public AnswerEntity? Answer { get; set; }
    public Guid? ParentCommentId { get; set; }
    public CommentEntity? ParentComment { get; set; }

    public ValueCollection<CommentEntity> SubComments { get; set; }
    public ValueCollection<FileEntity> Files { get; set; } = new();

    public override bool Equals(object? obj)
    {
        CommentEntity? discussion = obj as CommentEntity;
        if (GetHashCode() != obj?.GetHashCode()) return false;
        if (ParentComment is not null && discussion?.ParentComment is not null)
        {
            if (ParentComment.Equals(discussion?.ParentComment)) return false;
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
        return HashCode.Combine(Id, UserId, Text, ParentCommentId, CreationTime);
    }
}
