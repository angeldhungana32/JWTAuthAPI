using JWTAuthAPI.Entities.Identity;

namespace JWTAuthAPI.Interfaces
{
    public interface ITokenService
    {
        string GenerateAuthenticationToken(ApplicationUser user);
    }
}
