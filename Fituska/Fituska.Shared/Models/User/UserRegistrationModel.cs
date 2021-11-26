namespace Fituska.Shared.Models.User;

public record UserRegistrationModel : UserEditModel
{
    [Required(ErrorMessage = "Heslo musí být zadáno.")]
    [DataType(DataType.Password)]
    [StringLength(maximumLength: 64, ErrorMessage = "Délka hesla musí být mezi {2} a {1} znaky.", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [PasswordConfirmValidator]
    public string PasswordConfirm { get; set; } = string.Empty;

    private class PasswordConfirmValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext context)
        {
            return (context.ObjectInstance as UserRegistrationModel).Password == (string)value
                ? ValidationResult.Success
                : new ValidationResult("Zadané heslo se neshoduje.");
        }
    }
}
