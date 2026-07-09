using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Source.Application.Models.Requests;
using Source.Infrastructure.Persistence.Context;
using System.Security.Cryptography;
using System.Text;

namespace Source.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public AuthController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive);

            if (user is null)
            {
                return Unauthorized(new { message = "Credenciais inválidas." });
            }

            if (!VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Credenciais inválidas." });
            }

            var token = GenerateToken(user.Username);
            return Ok(new { token });
        }

        private static string GenerateToken(string username)
        {
            var payload = $"{username}:{DateTime.UtcNow:O}:{DateTime.UtcNow.AddMinutes(10):O}";
            var bytes = Encoding.UTF8.GetBytes(payload);
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            var passwordHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
            return string.Equals(passwordHash, storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
