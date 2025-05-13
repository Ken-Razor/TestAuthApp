using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuthServer.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Input Validation
            if (string.IsNullOrEmpty(request.Username))
                return BadRequest("Username/Email wajib diisi");

            if (!new EmailAddressAttribute().IsValid(request.Username))
                return BadRequest("Format email tidak valid");

            if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 6)
                return BadRequest("Password minimal 6 karakter");

            if (string.IsNullOrEmpty(request.FullName))
                return BadRequest("Nama lengkap wajib diisi");

            // Check Email Duplication
            if (await _userManager.FindByEmailAsync(request.Username) != null)
                return BadRequest("Email sudah terdaftar");

            // Create new User
            var user = new User
            {
                UserName = request.Username,
                Email = request.Username,
                FullName = request.FullName
            };

            // Save User
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new
                {
                    Title = "Password requirements not met",
                    Errors = errors
                });
            }

            // Response
            return Ok(new
            {
                Success = true,
                Message = "Registrasi berhasil",
                UserId = user.Id
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Input Validation
            if (string.IsNullOrEmpty(request.Username))
                return BadRequest("Username wajib diisi");

            if (string.IsNullOrEmpty(request.Password))
                return BadRequest("Password wajib diisi");

            // Check user
            var user = await _userManager.FindByEmailAsync(request.Username);
            if (user == null)
                return Unauthorized("Email belum terdaftar");

            // Check Status lockout
            if (await _userManager.IsLockedOutAsync(user))
            {
                var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                return Unauthorized(new
                {
                    Message = $"Akun terkunci hingga {lockoutEnd}",
                    IsLockedOut = true,
                    LockoutEnd = lockoutEnd
                });
            }

            // Check password
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                {
                    return Unauthorized(new
                    {
                        Message = "Akun terkunci setelah 3x percobaan gagal",
                        IsLockedOut = true,
                        RemainingAttempts = 0
                    });
                }

                var remainingAttempts = _userManager.Options.Lockout.MaxFailedAccessAttempts - (await _userManager.GetAccessFailedCountAsync(user));

                return Unauthorized(new
                {
                    Message = $"Password salah. Percobaan tersisa: {remainingAttempts}",
                    IsLockedOut = false,
                    RemainingAttempts = remainingAttempts
                });
            }

            // Reset counter if berhasil
            await _userManager.ResetAccessFailedCountAsync(user);

            // Generate token
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                Token = token,
                Expires = DateTime.Now.AddHours(1),
                FullName = user.FullName
            });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("full_name", user.FullName) // Claim custom
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}