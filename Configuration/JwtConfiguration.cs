namespace JWTAuthAPI.Configuration
{
    public class JwtConfiguration
    {
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public string? SecretKey { get; set; }
    }
}