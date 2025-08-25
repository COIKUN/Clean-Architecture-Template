using System.Text;
using Infrastructure.Data;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSetting>(options =>
        {
            options.Secret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ??
                             throw new InvalidOperationException("JWT_SECRET_KEY environment variable is not set");
            options.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ??
                             throw new InvalidOperationException("JWT_ISSUER environment variable is not set");
            options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ??
                               throw new InvalidOperationException("JWT_AUDIENCE environment variable is not set");
            options.LifeTime =
                int.Parse(Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_LIFETIME_MINUTES") ?? "15");
            options.RefreshTokenLifeTime =
                int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_LIFETIME_DAYS") ?? "7");
        });

        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!)),
                ClockSkew = TimeSpan.Zero
            };
        });


        return services;
    }
}