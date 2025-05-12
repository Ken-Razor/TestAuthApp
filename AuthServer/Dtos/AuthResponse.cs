namespace AuthServer.Dtos
{
    public class AuthResponse
    {
        public required string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}