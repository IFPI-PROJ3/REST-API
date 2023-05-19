using Microsoft.Extensions.DependencyInjection;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Services.Authentication.Commands;
using Proj3.Application.Common.Interfaces.Services.Authentication.Queries;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Application.Services.Authentication.Commands;
using Proj3.Application.Services.Authentication.Queries;
using Proj3.Application.Services.NGO.Commands;
using Proj3.Application.Services.NGO.Queries;

namespace Proj3.Application.Services.Authentication;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Authentication
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        services.AddScoped<IEmailCommandService, EmailCommandService>();

        // Common


        // NGO
        services.AddScoped<INgoCommandService, NgoCommandService>();
        services.AddScoped<INgoQueryService, NgoQueryService>();

        return services;
    }
}