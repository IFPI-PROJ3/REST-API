using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Services;
using Proj3.Infrastructure.Authentication.Utils;
using Proj3.Infrastructure.Authentication;
using Proj3.Infrastructure.Database;
using Proj3.Infrastructure.Repositories;
using Proj3.Infrastructure.Services;

namespace Proj3.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>();

        services.AddAuth(configuration);
        services.AddScoped<IEmailUtils, EmailUtils>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserValidationCodeRepository, UserValidationCodeRepository>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        JwtSettings? jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings); // json to jwtsettings

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<ITokensUtils, TokensUtils>();

        return services;
    }
}