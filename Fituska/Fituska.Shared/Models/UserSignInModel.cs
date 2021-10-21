namespace Fituska.Shared.Models;

public class UserSignInModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-mailová adresa")]
    public string EmailAddress { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(maximumLength: 64, ErrorMessage = "Délka hesla musí být mezi {2} a {1} znaky.", MinimumLength = 6)]
    [Display(Name = "Heslo")]
    public string Password { get; set; }
}
