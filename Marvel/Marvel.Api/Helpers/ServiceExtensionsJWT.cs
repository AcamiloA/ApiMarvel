using Marvel.Application.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Marvel.Api.Helpers
{
    /// <summary>
    /// Extensión de servicio para incluir validación del token JWT
    /// </summary>
    public static class ServiceExtensionsJWT
    {
        /// <summary>
        /// Extensión del servicio para incluir la validación del JWT Token
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddIdentityJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secSettingsSection = configuration.GetSection(nameof(JwtSecuritySettings));
            JwtSecuritySettings securitySettings = secSettingsSection.Get<JwtSecuritySettings>()!;

            var key = Encoding.ASCII.GetBytes(securitySettings.Secret!);
            var issuer = securitySettings.Issuer;
            var audience = securitySettings.Audience;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                o.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync("Usuario no autorizado");
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync("Usuario no tiene permisos");
                    }
                };
            });

        }
    }
}
