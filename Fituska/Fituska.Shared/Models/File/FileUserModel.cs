namespace Fituska.Shared.Models.File;

/// <summary> Holds user photo content (for loading data via generic FileLoader component). </summary>
/// <remarks> Not mapped to FileEntity </remarks>
public record FileUserModel : FileModelBase
{
    public Guid UserId { get; set; }

    public override bool ImageOnly() => true;
}
