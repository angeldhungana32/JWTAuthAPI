﻿using JWTAuthAPI.Entities.Identity;

namespace JWTAuthAPI.Entities.DTOs.Authentication
{
    public static class AuthenticationMappings
    {
        public static AuthenticateResponse ToResponseDTO(this ApplicationUser user, string token)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new AuthenticateResponse()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AuthToken = token
            };
        }
    }
}
