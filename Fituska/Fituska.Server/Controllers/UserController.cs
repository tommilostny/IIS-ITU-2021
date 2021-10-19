using Fituska.Server.Entities;
using Fituska.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fituska.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IConfiguration _configuration;

    /// <summary> fituska.net/api/user </summary>
    public UserController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    /// <summary> fituska.net/api/user/register </summary>
    [Route("register")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegistrationModel user)
    {
        // TODO: Use e-mail as username?
        // TODO: Proper user mapping.
        var identityUser = new UserEntity
        {
            Email = user.EmailAddress,
            UserName = user.EmailAddress,
            DiscordUsername = user.DiscordUsername,
            RegistrationDate = DateTime.UtcNow,
        };
        var userIdentityResult = await _userManager.CreateAsync(identityUser, user.Password);
        var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, user.RoleName);

        if (userIdentityResult.Succeeded && roleIdentityResult.Succeeded)
        {
            //TODO: PhotoRepository?
            //if (user.Photo is not null)
            //{
            //
            //}
            return Ok(new { userIdentityResult.Succeeded });
        }
        string errors = "Registrace selhala s následujícími chybami:";
        foreach (var error in userIdentityResult.Errors)
        {
            errors += $"\n{error.Code}: {error.Description}";
        }
        return BadRequest(errors);
    }

    /// <summary> fituska.net/api/user/signin </summary>
    [Route("signin")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] UserSignInModel user)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(user.EmailAddress, user.Password, isPersistent: false, lockoutOnFailure: false);
        if (signInResult.Succeeded)
        {
            var identityUser = await _userManager.FindByNameAsync(user.EmailAddress);
            var jsonWebToken = await CreateJsonWebToken(identityUser);
            return Ok(jsonWebToken);
        }
        return Unauthorized(user);
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    private async Task<string> CreateJsonWebToken(UserEntity identityUser)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString())
        };
        var roleNames = await _userManager.GetRolesAsync(identityUser);
        claims.AddRange(roleNames.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

        var jwtSecurityToken = new JwtSecurityToken
        (
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(28),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
