namespace Fituska.Shared.Models.File;

public record FileModelBase : ModelBase
{
    public string Name { get; set; }
    public byte[] Content { get; set; }

    public virtual bool ImageOnly() => false;
}
