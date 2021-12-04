namespace Fituska.Shared.Models.User;

public record UserPasswordChangeModel : UserSignInModel
{
    public string OldPassword { get; set;}
}
