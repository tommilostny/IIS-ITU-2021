namespace Fituska.Server.Entities;
public class PhotoEntity : EntityBase
{
    public byte[]? Content { get; set; } 

    public override bool Equals(object? obj)
    {
        if(obj == null) return false;
        if (GetHashCode() != obj.GetHashCode()) return false;
        PhotoEntity? photo = (PhotoEntity?)obj;
        if (Content == null && photo?.Content == null) return true; 
        bool? same= Content?.SequenceEqual(photo?.Content!);
        if(same is null) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
