using AuthorizationService.API.ViewModels;
using AuthorizationService.BLL.Interfaces;
using AuthorizationService.BLL.Models;
using AuthorizationService.DAL.Entities;
using AutoMapper;
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
    private readonly IUserService<User, int> _userService;
    private readonly IMapper _mapper;

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
                        new Claim("Email", user.Email)
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

    private async Task<LoginViewModel?> GetUser(string email, string password)
    {
        var users = await _userService.GetAll(default);
        var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
        
        return user is not null ? _mapper.Map<LoginViewModel>(user) : null;
    }
}
