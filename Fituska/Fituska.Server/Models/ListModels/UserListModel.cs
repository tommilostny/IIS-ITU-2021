namespace Fituska.Server.Models.ListModels
{
    public record UserListModel : ModelBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? PhotoID { get; set; }
        public PhotoEntity? Photo { get; set; }
    }
}
