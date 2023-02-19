using JWTAuthAPI.Entities.DTOs.Authentication;
using JWTAuthAPI.Entities.Identity;

namespace JWTAuthAPI.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser> GetUserByIdAsync(string id,
            CancellationToken cancellationToken = default);
        Task<ApplicationUser> AddUserAsync(ApplicationUser entity,
            CancellationToken cancellationToken = default);
        Task UpdateUserAsync(ApplicationUser entity,
            CancellationToken cancellationToken = default);
        Task DeleteUserAsync(string id,
            CancellationToken cancellationToken = default);
        Task<AuthenticateResponse> AuthenticateUserAsync(AuthenticateRequest request,
            CancellationToken cancellationToken = default);
    }
}
