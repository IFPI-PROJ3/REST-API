using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Proj3.Application.Common.Interfaces.Others;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Domain.Entities.Authentication;
using Proj3.Domain.Entities.Common;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;
using Proj3.Infrastructure.Authentication;
using Proj3.Infrastructure.Authentication.Utils;
using Proj3.Infrastructure.Persistence;
using Proj3.Infrastructure.Persistence.Repositories;
using Proj3.Infrastructure.Persistence.Repositories.Authentication;
using Proj3.Infrastructure.Persistence.Repositories.Common;
using Proj3.Infrastructure.Persistence.Repositories.NGO;
using Proj3.Infrastructure.Persistence.Repositories.Volunteer;
using Proj3.Infrastructure.Services;

namespace Proj3.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        //services.AddEntityFrameworkNpgsql();
        services.AddScoped<DbContext, AppDbContext>();
        services.AddScoped<ITransactionsManager, TransactionManager>();
        
        services.AddAuth(configuration);
        services.AddScoped<IEmailUtils, EmailUtils>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        // Authentication
        services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRepositoryBase<RefreshToken>, RepositoryBase<RefreshToken>>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IRepositoryBase<UserValidationCode>, RepositoryBase<UserValidationCode>>();
        services.AddScoped<IUserValidationCodeRepository, UserValidationCodeRepository>();        

        // Common
        services.AddScoped<IRepositoryBase<Category>, RepositoryBase<Category>>();
        services.AddScoped<IRepositoryBase<NgoCategory>, RepositoryBase<NgoCategory>>();
        services.AddScoped<IRepositoryBase<VolunteerCategory>, RepositoryBase<VolunteerCategory>>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserImageRepository, UserImageRepository>();

        // Review
        services.AddScoped<IRepositoryBase<Review>, RepositoryBase<Review>>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        // NGO
        services.AddScoped<IRepositoryBase<Ngo>, RepositoryBase<Ngo>>();
        services.AddScoped<INgoRepository, NgoRepository>();        
        services.AddScoped<IRepositoryBase<Event>, RepositoryBase<Event>>();
        services.AddScoped<IEventRepository, EventRepository>();

        // Images
        services.AddScoped<IRepositoryBase<EventImage>, RepositoryBase<EventImage>>();
        services.AddScoped<IEventImagesRepository, EventImagesRepository>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        JwtSettings? jwtSettings = new();
        configuration.Bind(JwtSettings.SectionName, jwtSettings); // json to jwtsettings

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<ITokensUtils, TokensUtils>();

        return services;
    }
}