namespace Fituska.Shared.Models.User;

public record UserListModel : ModelBase
{
    public string UserName { get; set; }
    public string? PhotoUrl { get; set; }
}
