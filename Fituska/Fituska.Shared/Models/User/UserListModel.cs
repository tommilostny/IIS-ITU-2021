namespace Fituska.Shared.Models;

public record UserListModel : ModelBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid? PhotoID { get; set; }
    //public PhotoEntity? Photo { get; set; }
    public string Email { get; set; }
}
