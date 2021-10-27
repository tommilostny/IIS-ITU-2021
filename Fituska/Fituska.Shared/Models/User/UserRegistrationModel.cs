namespace Fituska.Shared.Models.User;
public record UserRegistrationModel : UserSignInModel
{
    [StringLength(maximumLength: 64)]
    [Display(Name = "Discord")]
    public string DiscordUsername { get; set; }

    [DataType(DataType.Upload)]
    [Display(Name = "Profilový obrázek")]
    public byte[]? Photo { get; set; }

    [RoleNameValidator]
    public string RoleName { get; set; }

    private class RoleNameValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext _)
        {
            var role = value as string;

            return RoleNames.GetAll().Contains(role)
                ? ValidationResult.Success!
                : new ValidationResult($"Role '{role}' neexistuje.");
        }
    }
}
