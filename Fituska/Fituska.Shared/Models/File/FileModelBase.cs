namespace Fituska.Shared.Models.File;

public abstract record FileModelBase : ModelBase
{
    public string Name { get; set; }
    public byte[] Content { get; set; }
}
