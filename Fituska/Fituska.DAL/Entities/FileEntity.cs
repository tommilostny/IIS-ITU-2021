namespace Fituska.DAL.Entities;
public class FileEntity : EntityBase
{
    public string Name { get; set; }

    public byte[]? Content { get; set; }

    public Guid? QuestionId { get; set; }

    public QuestionEntity? Question { get; set; }

    public Guid? AnswerId { get; set; }

    public AnswerEntity? Answer { get; set; }

    public Guid? DiscussionId { get; set; }

    public DiscussionEntity? Discussion { get; set; }

    public override bool Equals(object? obj)
    {
        FileEntity? file = obj as FileEntity;
        if (GetHashCode() != file?.GetHashCode()) return false;
        if (Content == null && file?.Content == null) return true;
        bool? same = Content?.SequenceEqual(file?.Content!);
        if (same is null || same == false) return false;
        if ((Answer is null && file?.Answer is not null) || (Answer is not null && file?.Answer is null)) return false;
        if ((Discussion is null && file?.Discussion is not null) || (Discussion is not null && file?.Discussion is null)) return false;
        if ((Question is null && file?.Question is not null) || (Question is not null && file?.Question is null)) return false;
        if (Answer is not null && file?.Answer is not null)
        {
            if ((bool)!Answer?.Equals(file?.Answer)) return false;
        }
        if (Discussion is not null && file?.Discussion is not null)
        {
            if ((bool)!Discussion?.Equals(file?.Discussion)) return false;
        }
        if (Question is not null && file?.Question is not null)
        {
            if ((bool)!Question?.Equals(file?.Question)) return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}
