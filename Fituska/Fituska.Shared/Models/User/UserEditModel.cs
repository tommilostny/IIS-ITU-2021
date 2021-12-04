namespace Fituska.Shared.Models.User;

public record UserEditModel : UserModelBase
{
    [Required(ErrorMessage = "E-mailová adresa musí být zadána.")]
    [EmailAddress(ErrorMessage = "Zadaná e-mailová adresa není ve správném formátu.")]
    [VutbrEmailValidator]
    public string Email { get; set; }

    [Required(ErrorMessage = "Jméno musí být zadáno.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Příjmení musí být zadáno")]
    public string LastName { get; set; }

    [MaxLength(37, ErrorMessage = "Discord uživatelské jméno nesmí být delší než 37 znaků.")]
    public string? DiscordUsername { get; set; }

    private class VutbrEmailValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext context)
        {
            string[] validDomains = { "vutbr.cz", "vut.cz", "fituska.net" };

            return validDomains.Any(d => (value as string).EndsWith(d))
                ? ValidationResult.Success
                : new ValidationResult($"E-mailová adresa musí končit: {string.Join(',', validDomains)}" );
        }
    }
}
