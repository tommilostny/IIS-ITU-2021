using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fituska.Client.Providers;

public class FituskaAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly ILocalStorageService _localStorageService;

    public FituskaAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var savedToken = await _localStorageService.GetItemAsStringAsync(StorageNames.BearerToken);
            // No token stored in local storage => no user is signed in, return empty authentication state.
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return EmptyAuthenticationState();
            }
            var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            // Check validity of the loaded Token (expired => remove from local storage).
            if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
            {
                await _localStorageService.RemoveItemAsync(StorageNames.BearerToken);
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

    internal async Task<Guid> SignIn()
    {
        var savedToken = await _localStorageService.GetItemAsync<string>(StorageNames.BearerToken);
        var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        var claims = ParseClaims(jwtSecurityToken);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        var authenticationState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authenticationState);

        return new Guid(user.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
    }

    internal void SignOut()
    {
        var noUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authenticationState = Task.FromResult(new AuthenticationState(noUser));
        NotifyAuthenticationStateChanged(authenticationState);
    }
}
