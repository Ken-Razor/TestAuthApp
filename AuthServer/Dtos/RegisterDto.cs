using System.ComponentModel.DataAnnotations;


namespace AuthServer.Dtos
{
    public class RegisterDto
{
    [Required(ErrorMessage = "Username/Email wajib diisi")]
    [EmailAddress(ErrorMessage = "Format email tidak valid")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password wajib diisi")]
    [MinLength(6, ErrorMessage = "Password minimal 6 karakter")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", 
        ErrorMessage = "Password harus mengandung huruf besar dan angka")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Nama lengkap wajib diisi")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Nama minimal 3 karakter")]
    public string FullName { get; set; }
}
}