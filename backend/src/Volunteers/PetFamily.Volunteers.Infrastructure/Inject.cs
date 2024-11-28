using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Core.Messaging;
using PetFamily.Core.Providers;
using PetFamily.Infrastructure.BackGroundServices;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Infrastructure.BackgroundServices;
using PetFamily.Volunteers.Infrastructure.Data;
using PetFamily.Volunteers.Infrastructure.Files;
using PetFamily.Volunteers.Infrastructure.Messaging;
using PetFamily.Volunteers.Infrastructure.Options;
using PetFamily.Volunteers.Infrastructure.Services;
using System.Runtime.CompilerServices;
using FileInfo = PetFamily.Core.Providers.FileInfo;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddVolunteersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddMinio(configuration)
            .AddRepositories()
            .AddDatabase()
            .AddHostedServices()
            .AddMessaging()
            .AddSingleton<IFilesCleanerService, FilesCleanerService>()
            .AddScoped<ISoftDeletableVolunteersEntitiesCleanerService, SoftDeletableVolunteersEntitiesCleanerService>();

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


    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();

        return services;
    }

    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<FilesCleanUpService>();
        services.AddHostedService<SoftDeletableEntitiesCleanUpService>();

        return services;
    }

    public static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddSingleton<IFileProvider, MinioProvider>();

        return services;
    }
}