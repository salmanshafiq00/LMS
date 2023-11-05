using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.OptionsSetup;
using Web.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IUser, CurrentUser>();

        services.AddCors(o =>
            o.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            }));

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        // swagger configure
        services.ConfigureOptions<SwaggerOptionsSetup>();

        return services;
    }
}
