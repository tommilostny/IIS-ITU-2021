using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Fituska.Shared.Static;

public static class GetUserInfo
{
    public static async Task<Guid> GetUserId(Task<AuthenticationState> AuthenticationState)
    {
        Guid userId;
        try {
            userId = new Guid((await AuthenticationState).User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
        }
        catch{
            userId = Guid.Empty;
        }
        return userId;
    }
}
