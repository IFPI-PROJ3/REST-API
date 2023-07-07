using Microsoft.AspNetCore.Identity;
using Proj3.Api.Extensions;
using Proj3.Api.Middlewares;
using Proj3.Api.Middlewares.Authentication;
using Proj3.Application.Services.Authentication;
using Proj3.Infrastructure;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddSwagger();

    // MUDAR DE LOCAL
    builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(2));
}

WebApplication? app = builder.Build();
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    app.UseHttpsRedirection();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    
    app.UseWhen(context => context.Request.Path.StartsWithSegments("/auth/logout"), appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();
    });

    app.UseWhen(context => context.Request.Path.StartsWithSegments("/auth/change-password"), appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();
    });


    app.UseWhen(context => context.Request.Path.StartsWithSegments("/ngo"), appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();
    });


    app.UseWhen(context => context.Request.Path.StartsWithSegments("/event"), appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();
    });

    app.UseWhen(context => context.Request.Path.StartsWithSegments("/event-volunteers"), appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();
    });

    app.UseWhen(context => context.Request.Path.StartsWithSegments("/volunteer"), appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();
    });

    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Run();
}
