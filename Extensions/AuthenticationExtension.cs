using JWTAuthAPI.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWTAuthAPI.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfig"));
            
            var jwtConfiguration = configuration.GetSection("JwtConfig").Get<JwtConfiguration>();
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
