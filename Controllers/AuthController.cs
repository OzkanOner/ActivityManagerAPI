using ActivityManagerAPI.Data;
using ActivityManagerAPI.Helpers;
using ActivityManagerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
        public async Task<IActionResult> GenerateTokenAsync([FromBody] Login loginModel)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginModel.Username);

            if (user == null)
                return Unauthorized("User not found!");

            bool isPasswordValid = PasswordHelper.VerifyPasswordHash(loginModel.Password, user.PasswordHash, user.PasswordSalt);
            if (!isPasswordValid)
                return Unauthorized("Password is not correct!");

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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == registerModel.Username || u.Email == registerModel.Email);
            if (user != null)
                return BadRequest("User name or email already exists!");

            PasswordHelper.CreatePasswordHash(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Username = registerModel.Username,
                Email = registerModel.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }
    }
}