// Shared/AuthModels.cs
using System.ComponentModel.DataAnnotations;

namespace AuthClient.Shared
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username/Email wajib diisi")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password wajib diisi")]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username/Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password wajib diisi")]
        [MinLength(6, ErrorMessage = "Password minimal 6 karakter")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nama lengkap wajib diisi")]
        public string FullName { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public string FullName { get; set; } = string.Empty;
    }

    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }

    public class ErrorResponse
    {
        // For login errors
        public string Message { get; set; } = string.Empty;
        public bool IsLockedOut { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public int? RemainingAttempts { get; set; }

        // For registration
        public string Title { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

}