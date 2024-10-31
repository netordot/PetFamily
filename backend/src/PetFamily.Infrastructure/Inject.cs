using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Database;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Application.Species;
using PetFamily.Application.Volunteers;
using PetFamily.Infrastructure.BackGroundServices;
using PetFamily.Infrastructure.Interceptors;
using PetFamily.Infrastructure.MessageQueues;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;
using System.Runtime.CompilerServices;
using FileInfo = PetFamily.Application.Providers.FileProvider.FileInfo;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WriteDbContext>();
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddScoped<IFileProvider, MinioProvider>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        services.AddMinio(configuration);

        return services;
    }

    public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
            ?? throw new ApplicationException("Missing MinIo configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(false);
        });

        return services;
    }
}