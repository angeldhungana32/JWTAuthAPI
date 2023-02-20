using JWTAuthAPI.Entities.DTOs.Authentication;
using JWTAuthAPI.Entities.Identity;

namespace JWTAuthAPI.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> AddUserAsync(ApplicationUser entity, string password);
        Task<bool> UpdateUserAsync(ApplicationUser entity);
        Task<bool> DeleteUserAsync(ApplicationUser entity);
        Task<AuthenticateResponse?> AuthenticateUserAsync(AuthenticateRequest request);
    }
}
