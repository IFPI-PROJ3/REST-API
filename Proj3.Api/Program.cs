using Proj3.Api.Middlewares.Authentication;
using Proj3.Api.Middlewares;
using Proj3.Application.Services.Authentication;
using Proj3.Infrastructure;
using Proj3.Api.Extensions;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddSwagger();
}

WebApplication? app = builder.Build();
{

    app.UseHttpsRedirection();

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseWhen(context => context.Request.Path.StartsWithSegments("/ngo"), appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();
    });

    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Run();
}
