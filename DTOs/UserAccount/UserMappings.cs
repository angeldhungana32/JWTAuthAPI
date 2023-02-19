﻿using JWTAuthAPI.DTOs.Authentication;
using JWTAuthAPI.Entities.Identity;

namespace JWTAuthAPI.DTOs.UserAccount
{
    public static class UserMappings
    {
        public static UserResponse ToResponseDTO(this ApplicationUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new UserResponse()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static ApplicationUser ToEntity(this UserCreateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return new ApplicationUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };
        }
    }
}
