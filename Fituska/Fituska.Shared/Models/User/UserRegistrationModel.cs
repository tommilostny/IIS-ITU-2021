namespace Fituska.Shared.Models.User;
public record UserRegistrationModel : UserSignInModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-mailová adresa")]
    public string Email { get; set; }

    [Required, Display(Name = "Jméno")]
    public string FirstName { get; set; }

    [Required, Display(Name = "Příjmení")]
    public string LastName { get; set; }

    [StringLength(maximumLength: 64)]
    [Display(Name = "Discord")]
    public string? DiscordUsername { get; set; }

    [DataType(DataType.Upload)]
    [Display(Name = "Profilový obrázek")]
    public byte[]? Photo { get; set; }
}
