using Microsoft.Extensions.DependencyInjection;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Services.Authentication.Commands;
using Proj3.Application.Common.Interfaces.Services.Authentication.Queries;
using Proj3.Application.Common.Interfaces.Services.Common.Command;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Commands;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Queries;
using Proj3.Application.Services.Authentication.Commands;
using Proj3.Application.Services.Authentication.Queries;
using Proj3.Application.Services.Common.Command;
using Proj3.Application.Services.Common.Queries;
using Proj3.Application.Services.NGO.Commands;
using Proj3.Application.Services.NGO.Queries;
using Proj3.Application.Services.Volunteer.Commands;
using Proj3.Application.Services.Volunteer.Queries;

namespace Proj3.Application.Services.Authentication;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {        
        // Authentication
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        services.AddScoped<IEmailCommandService, EmailCommandService>();        

        // Category
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();
        
        // NGO
        services.AddScoped<INgoCommandService, NgoCommandService>();
        services.AddScoped<INgoQueryService, NgoQueryService>();
        services.AddScoped<IEventCommandService, EventCommandService>();
        services.AddScoped<IEventQueryService, EventQueryService>();
        
        // Review
        services.AddScoped<IReviewCommandService, ReviewCommandService>();
        services.AddScoped<IReviewQueryService, ReviewQueryService>();
        
        // EventVolunteer
        services.AddScoped<IEventVolunteerCommandService, EventVolunteerCommandService>();
        services.AddScoped<IEventVolunteerQueryService, EventVolunteerQueryService>();

        return services;
    }
}