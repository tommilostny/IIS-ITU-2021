namespace Fituska.Shared.Models.User;

public record UserSignInModel : UserModelBase
{
    [Required(ErrorMessage = "Heslo musí být zadáno.")]
    [DataType(DataType.Password)]
    [StringLength(maximumLength: 64, ErrorMessage = "Délka hesla musí být mezi {2} a {1} znaky.", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}
