using JWTAuthAPI.Entities.Identity;

namespace JWTAuthAPI.DTOs.Authentication
{
    public class AuthenticateResponse
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AuthToken { get; set; }
    }
}
