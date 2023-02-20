﻿using JWTAuthAPI.Authorization;
using JWTAuthAPI.Entities.DTOs.Authentication;
using JWTAuthAPI.Entities.DTOs.UserAccount;
using JWTAuthAPI.Entities.Identity;
using JWTAuthAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JWTAuthAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public AccountService(UserManager<ApplicationUser> userManager, ITokenService tokenService,
            IAuthorizationService authorizationService) 
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authorizationService = authorizationService;
        }

        public async Task<ApplicationUser?> AddUserAsync(ApplicationUser entity, string password)
        {
            await _userManager.CreateAsync(entity, password);
            var validUser = await _userManager.FindByEmailAsync(entity.Email);
            return validUser;
        }

        public async Task<AuthenticateResponse?> AuthenticateUserAsync(AuthenticateRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var isValidUser = await _userManager.CheckPasswordAsync(user, request.Password);
                if (isValidUser)
                {
                    string token = _tokenService.GenerateAuthenticationToken(user);
                    return user.ToResponseDTO(token);
                }
            }

            return default;
        }

        public async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> AuthorizeOwnerAsync(ClaimsPrincipal userContext, ApplicationUser resource)
        {
            var authorized = await _authorizationService.AuthorizeAsync(userContext, resource,
                    new UserIsOwnerRequirement());

            return authorized.Succeeded;
        }
    }
}
