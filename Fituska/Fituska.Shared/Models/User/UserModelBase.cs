namespace Fituska.Shared.Models.User;

public record UserModelBase : ModelBase
{
    [Required]
    [MinLength(3)]
    [Display(Name = "Uživatelské jméno")]
    public string UserName { get; set; }
}
