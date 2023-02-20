using JWTAuthAPI.Entities.DTOs.Authentication;
using JWTAuthAPI.Entities.Identity;
using System.Security.Claims;

namespace JWTAuthAPI.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> AddUserAsync(ApplicationUser entity, string password);
        Task<bool> UpdateUserAsync(ApplicationUser entity);
        Task<bool> DeleteUserAsync(ApplicationUser entity);
        Task<AuthenticateResponse?> AuthenticateUserAsync(AuthenticateRequest request);
        Task<bool> AuthorizeOwnerAsync(ClaimsPrincipal userContext, ApplicationUser resource);
    }
}
