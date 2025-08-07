using JwtAuthDemo.Models;
using JwtAuthDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    public UserService _userservice;

    public AuthController(IConfiguration config, UserService userService)
    {
        _config = config;
        _userservice = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
        {
            return BadRequest("Invalid login request");
        }
        try {
            User user = _userservice.GetUserByEmail(login.Email);
            if (user == null)
            {
                return Unauthorized("User not found");
            }
            if (login.Email == user.Email && login.Password == user.Password)
            {
                var currentUser = _userservice.GetUserById(user.Id);
                var token = GenerateJwtToken(login.Email);
                return Ok(new
                {
                    token,
                    user = currentUser
                });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving user: {ex.Message}");
        }


       
        

        return Unauthorized();
    }

    private string GenerateJwtToken(string username)
    {
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    [HttpPost("test")]
    public User test([FromBody] String email)
    {
      return  _userservice.GetUserByEmail(email);

    }
}
