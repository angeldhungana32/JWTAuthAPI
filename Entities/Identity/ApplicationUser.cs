﻿using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [ProtectedPersonalData]
        public virtual string? FirstName { get; set; }

        [ProtectedPersonalData]
        public virtual string? LastName { get; set; }
    }
}
