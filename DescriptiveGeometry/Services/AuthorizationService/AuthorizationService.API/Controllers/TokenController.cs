using System.Diagnostics.Tracing;
using AuthorizationService.API.Models;
using AuthorizationService.API.ViewModels;
using AuthorizationService.BLL.Interfaces;
using AuthorizationService.BLL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationService.API.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUserService<User, int> _userService;
    private readonly IMapper _mapper;
    private const int TokenLifetime = 10;

    public TokenController(IConfiguration config, IUserService<User, int> userService, IMapper mapper)
    {
        _configuration = config;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post(LoginViewModel userViewModel)
    {
        if (userViewModel is not null &&
            !string.IsNullOrEmpty(userViewModel.Email) && 
            !string.IsNullOrEmpty(userViewModel.Password))
        {
            var user = await GetUser(userViewModel.Email, userViewModel.Password);

            if (user is not null)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                    };

                var accessToken = CreateToken(claims);
                var refreshToken = GenerateRefreshToken();

                return Ok(
                    new{
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    RefreshToken = refreshToken
                    });
            }

            return BadRequest("Invalid credentials");
        }

        return BadRequest();
    }

    //[HttpPost]
    //[Route("login")]
    //public async Task<IActionResult> Login([FromBody] LoginModel model)
    //{
    //    var user = await _userManager.FindByNameAsync(model.Username);
    //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
    //    {
    //        var userRoles = await _userManager.GetRolesAsync(user);

    //        var authClaims = new List<Claim>
    //        {
    //            new Claim(ClaimTypes.Name, user.UserName),
    //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //        };

    //        foreach (var userRole in userRoles)
    //        {
    //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
    //        }

    //        var token = CreateToken(authClaims);
    //        var refreshToken = GenerateRefreshToken();

    //        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

    //        user.RefreshToken = refreshToken;
    //        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

    //        await _userManager.UpdateAsync(user);

    //        return Ok(new
    //        {
    //            Token = new JwtSecurityTokenHandler().WriteToken(token),
    //            RefreshToken = refreshToken,
    //            Expiration = token.ValidTo
    //        });
    //    }
    //    return Unauthorized();
    //}

    //[HttpPost]
    //[Route("refresh-token")]
    //public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
    //{
    //    if (tokenModel is null)
    //    {
    //        return BadRequest("Invalid client request");
    //    }

    //    string? accessToken = tokenModel.AccessToken;
    //    string? refreshToken = tokenModel.RefreshToken;

    //    var principal = GetPrincipalFromExpiredToken(accessToken);
    //    if (principal == null)
    //    {
    //        return BadRequest("Invalid access token or refresh token");
    //    }
        
    //    var email = principal?.Identity?.Name;

    //    if (email is null)
    //    {
    //        return BadRequest("Invalid access token or refresh token");
    //    }

    //    var user = await _userService.GetByEmail(email, default);

    //    if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
    //    {
    //        return BadRequest("Invalid access token or refresh token");
    //    }

    //    var newAccessToken = CreateToken(principal.Claims.ToList());
    //    var newRefreshToken = GenerateRefreshToken();

    //    user.RefreshToken = newRefreshToken;
    //    await _userManager.UpdateAsync(user);

    //    return new ObjectResult(new
    //    {
    //        accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
    //        refreshToken = newRefreshToken
    //    });
    //}

    private async Task<LoginViewModel?> GetUser(string email, string password)
    {
        var users = await _userService.GetAll(default);
        var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
        
        return user is not null ? _mapper.Map<LoginViewModel>(user) : null;
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;

    }

    private JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(TokenLifetime),
            claims: authClaims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
