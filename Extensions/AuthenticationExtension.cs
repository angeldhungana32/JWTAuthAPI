﻿using JWTAuthAPI.Configuration;
using JWTAuthAPI.Entities.Identity;
using JWTAuthAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWTAuthAPI.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));
            var jwtConfiguration = configuration.GetSection("Jwt").Get<JwtConfiguration>();
            
            byte[] key = Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = jwtConfiguration.RequireHttps;
                options.SaveToken = jwtConfiguration.SaveToken;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = jwtConfiguration.ValidateIssuer,
                    ValidateAudience = jwtConfiguration.ValidateAudience,
                    ValidateLifetime = jwtConfiguration.ValidateLifeTime,
                    ValidateIssuerSigningKey = jwtConfiguration.ValidateIssuerSigningKey,
                };
            });

            return services;
        }

        public static IApplicationBuilder UseJwtAuthorization(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
