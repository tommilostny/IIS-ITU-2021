namespace Fituska.Shared.Models.User;
public record UserListModel : ModelBase
{
    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid? PhotoID { get; set; }
    public byte[]? Photo { get; set; }
    public string? Email { get; set; }
}
