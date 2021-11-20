using Fituska.Shared.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly SignInManager<UserEntity> signInManager;
    private readonly UserManager<UserEntity> userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper mapper;

    /// <summary> fituska.net/api/user </summary>
    public UserController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IConfiguration configuration, IMapper mapper)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.configuration = configuration;
        this.mapper = mapper;
    }

    /// <summary> fituska.net/api/user/register </summary>
    [Route("register")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegistrationModel user)
    {
        var identityUser = mapper.Map<UserEntity>(user);
        var userIdentityResult = await userManager.CreateAsync(identityUser, user.Password);

        if (userIdentityResult.Succeeded)
        {
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
        var signInResult = await signInManager.PasswordSignInAsync(user.UserName, user.Password, isPersistent: false, lockoutOnFailure: false);
        if (signInResult.Succeeded)
        {
            var identityUser = await userManager.FindByNameAsync(user.UserName);
            var jsonWebToken = await CreateJsonWebToken(identityUser);
            return Ok(jsonWebToken);
        }
        return Unauthorized(user);
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    private async Task<string> CreateJsonWebToken(UserEntity identityUser)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString()),
        };
        var roleNames = await userManager.GetRolesAsync(identityUser);
        claims.AddRange(roleNames.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

        var jwtSecurityToken = new JwtSecurityToken
        (
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(28),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    [Route(nameof(GetAll))]
    [HttpGet]
    public IActionResult GetAll() => Ok(mapper.Map<List<UserListModel>>(userManager.Users.ToList()));

    [Route("{username}")]
    [HttpGet]
    public async Task<IActionResult> Get(string username)
    {
        var user = mapper.Map<UserDetailModel>(await userManager.FindByNameAsync(username));
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> Delete(string username)
    {
        var userToDelete = await userManager.FindByNameAsync(username);
        if (userToDelete is not null)
        {
            await userManager.DeleteAsync(userToDelete);
            return Ok();
        }
        return NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserEditModel user)
    {
        var entity = await userManager.FindByIdAsync(user.Id.ToString());

        entity.DiscordUsername = user.DiscordUsername;
        entity.Email = user.Email;
        entity.FirstName = user.FirstName;
        entity.LastName = user.LastName;
        entity.PhotoUrl = user.PhotoUrl;

        await userManager.UpdateAsync(entity);
        return Ok();
    }
}
