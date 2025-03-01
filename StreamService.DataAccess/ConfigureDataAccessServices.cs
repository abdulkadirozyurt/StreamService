using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.DataAccess.Concrete.EntityFramework;

namespace StreamService.DataAccess;

public static class ConfigureDataAccessServices
{
    public static IServiceCollection RegisterDataAccessServices(this IServiceCollection services, MongoDbSettings mongoDbSettings)
    {
        services.AddSingleton(mongoDbSettings);
        services.AddDbContext<MongoDbContext>(
            (sp, options) =>
            {
                if (string.IsNullOrWhiteSpace(mongoDbSettings.ConnectionString) || string.IsNullOrWhiteSpace(mongoDbSettings.DatabaseName))
                {
                    throw new InvalidOperationException("MongoDbSettings içinde ConnectionString veya DatabaseName eksik.");
                }

                options.UseMongoDB(mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName);
            }
        );

        // Geçici olarak bu satırı kaldırıyoruz, gerekli tipler oluşturulduğunda eklenebilir
        services.AddScoped<IUserDal, UserDal>();
        services.AddScoped<IStreamInformationDal, StreamInformationDal>();

        return services;
    }
}
