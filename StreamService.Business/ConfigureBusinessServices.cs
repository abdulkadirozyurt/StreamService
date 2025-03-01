using System;
using Microsoft.Extensions.DependencyInjection;
using StreamService.Business.Abstract;
using StreamService.Business.Concrete;

namespace StreamService.Business;

public static class ConfigureBusinessServices
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserManager>();
        // services.AddScoped<IStreamInformationBusiness, StreamInformationBusiness>();

        return services;
    }
}
