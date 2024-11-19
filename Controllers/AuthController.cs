using ActivityManagerAPI.Data;
using ActivityManagerAPI.Helpers;
using ActivityManagerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActivityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly AppDbContext _context;

        public AuthController(JwtSettings jwtSettings, AppDbContext context)
        {
            _jwtSettings = jwtSettings;
            _context = context;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateTokenAsync([FromBody] Login login)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null)
            {
                return Unauthorized("User not found!");
            }

            if (!PasswordHelper.VerifyPasswordHash(login.Password, user.PasswordHash))
            {
                return Unauthorized("Password is not correct!");
            }

            var claims = new[]
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register registerModel)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(registerModel.Password)));

                var newUser = new User
                {
                    Username = registerModel.Username,
                    PasswordHash = passwordHash,
                    Email = registerModel.Email
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok("User registered!");
            }
        }
    }
}