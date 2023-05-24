using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Proj3.Application.Common.Interfaces.Others;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Domain.Entities.Authentication;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;
using Proj3.Infrastructure.Authentication;
using Proj3.Infrastructure.Authentication.Utils;
using Proj3.Infrastructure.Persistence;
using Proj3.Infrastructure.Persistence.Repositories;
using Proj3.Infrastructure.Persistence.Repositories.Authentication;
using Proj3.Infrastructure.Persistence.Repositories.Ngo;
using Proj3.Infrastructure.Services;

namespace Proj3.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddTransient<DbContext, AppDbContext>();
        services.AddScoped<ITransactionsManager, TransactionManager>();

        services.AddAuth(configuration);
        services.AddScoped<IEmailUtils, EmailUtils>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        // Common

        // Authentication
        services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRepositoryBase<RefreshToken>, RepositoryBase<RefreshToken>>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IRepositoryBase<UserValidationCode>, RepositoryBase<UserValidationCode>>();
        services.AddScoped<IUserValidationCodeRepository, UserValidationCodeRepository>();

        // NGO
        services.AddScoped<IRepositoryBase<Ngo>, RepositoryBase<Ngo>>();
        services.AddScoped<INgoRepository, NgoRepository>();

        // Volunteer        
        

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