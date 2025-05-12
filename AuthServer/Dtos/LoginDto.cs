using System.ComponentModel.DataAnnotations;


namespace AuthServer.Dtos
{
    public class LoginDto
{
    [Required(ErrorMessage = "Username/Email wajib diisi")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password wajib diisi")]
    public string Password { get; set; }
}
}