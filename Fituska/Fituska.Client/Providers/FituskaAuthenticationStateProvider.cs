using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fituska.Client.Providers;

public class FituskaAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
    private readonly ILocalStorageService localStorageService;

    public FituskaAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var savedToken = await localStorageService.GetItemAsStringAsync(JwtNames.BearerToken);
            // No token stored in local storage => no user is signed in, return empty authentication state.
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return EmptyAuthenticationState();
            }
            var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            // Check validity of the loaded Token (expired => remove from local storage).
            if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
            {
                await localStorageService.RemoveItemAsync(JwtNames.BearerToken);
                return EmptyAuthenticationState();
            }
            // Get claims from the JWT and create authenticated user object.
            var claims = ParseClaims(jwtSecurityToken);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            return new AuthenticationState(user);
        }
        catch
        {
            return EmptyAuthenticationState();
        }
    }

    private static IList<Claim> ParseClaims(JwtSecurityToken jwtSecurityToken)
    {
        var claims = jwtSecurityToken.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, jwtSecurityToken.Subject));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, ClaimTypes.Name));
        return claims;
    }

    private static AuthenticationState EmptyAuthenticationState() => new(new ClaimsPrincipal(new ClaimsIdentity()));

    internal async Task SignIn()
    {
        var savedToken = await localStorageService.GetItemAsync<string>(JwtNames.BearerToken);
        var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        var claims = ParseClaims(jwtSecurityToken);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        var authenticationState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authenticationState);
    }

    internal void SignOut()
    {
        var noUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authenticationState = Task.FromResult(new AuthenticationState(noUser));
        NotifyAuthenticationStateChanged(authenticationState);
    }
}
