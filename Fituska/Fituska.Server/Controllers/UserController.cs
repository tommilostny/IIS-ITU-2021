using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Fituska.Server.Models.ListModels;
using Fituska.DAL.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Fituska.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly SignInManager<UserEntity> signInManager;
    private readonly UserManager<UserEntity> userManager;
    private readonly IConfiguration configuration;
    private readonly UserRepository userRepository;
    private readonly IMapper mapper;
    /// <summary> fituska.net/api/user </summary>
    public UserController(SignInManager<UserEntity> _signInManager, UserManager<UserEntity> _userManager, IConfiguration _configuration, UserRepository _userRepository, IMapper _mapper)
    {
        signInManager = _signInManager;
        userManager = _userManager;
        configuration = _configuration;
        userRepository = _userRepository;
        mapper = _mapper;
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
        var userIdentityResult = await userManager.CreateAsync(identityUser, user.Password);

        if (userIdentityResult.Succeeded)
        {
            var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, user.RoleName);
            if (roleIdentityResult.Succeeded)
            {
                var success = userIdentityResult.Succeeded && roleIdentityResult.Succeeded;
                return Ok(new { success });
            }
            //TODO: PhotoRepository?
            //if (user.Photo is not null)
            //{
            //
            //}
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
        var signInResult = await signInManager.PasswordSignInAsync(user.EmailAddress, user.Password, isPersistent: false, lockoutOnFailure: false);
        if (signInResult.Succeeded)
        {
            var identityUser = await userManager.FindByNameAsync(user.EmailAddress);
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
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString())
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

    [HttpGet]
    [Route("user" + nameof(GetAll))]
    public ActionResult<List<UserListModel>> GetAll()
    {
        return mapper.Map<List<UserListModel>>(userRepository.GetAll());
    }
}
