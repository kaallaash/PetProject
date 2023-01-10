using AuthorizationService.API.ViewModels;
using AuthorizationService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizationService.API.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public TokenController(IConfiguration config, IUserService userService)
    {
        _configuration = config;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserViewModel userViewModel)
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
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("Email", user.Email),
                        new Claim("Role", user.Role)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
        else
        {
            return BadRequest();
        }
    }

    private async Task<UserViewModel> GetUser(string email, string password)
    {
        var users = await _userService.GetAll(default);
        var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

        if (user == null)
        {
            return null;
        }

        var userViewModel = new UserViewModel()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role.ToString()
        };
        
        return userViewModel;
    }
}
