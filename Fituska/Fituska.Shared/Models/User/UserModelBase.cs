namespace Fituska.Shared.Models.User;

public abstract record UserModelBase : ModelBase
{
    [Required(ErrorMessage = "Uživatelské jméno musí být zadáno.")]
    [StringLength(maximumLength: 64, ErrorMessage = "Délka uživatelského jména musí být mezi {2} a {1} znaky.", MinimumLength = 2)]
    public string UserName { get; set; }
}
