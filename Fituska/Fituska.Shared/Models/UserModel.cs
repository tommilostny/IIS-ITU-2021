using System.ComponentModel.DataAnnotations;

namespace Fituska.Shared.Models;

public class UserModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-mailová adresa")]
    public string EmailAddress { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(maximumLength: 100, ErrorMessage = "Délka hesla musí být mezi {2} a {1} znaky.", MinimumLength = 6)]
    [Display(Name = "Heslo")]
    public string Password { get; set; }
}
