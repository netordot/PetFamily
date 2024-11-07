using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.Interceptors;

namespace PetFamily.Infrastructure;

public class ReadDbContext(IConfiguration configuration,
    SoftDeleteInterceptor softDeleteInterceptor) : DbContext, IReadDbContext
{
    public DbSet<VolunteerDto> Volunteers => Set<VolunteerDto>();
    public DbSet<SpeciesDto> Species => Set<SpeciesDto>();
    public DbSet<PetDto> Pets => Set<PetDto>(); 
    public DbSet<BreedDto> Breeds => Set<BreedDto>();   

    private const string DATABASE = nameof(Database);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(softDeleteInterceptor);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    private ILoggerFactory CreateLoggerFactory()
    => LoggerFactory.Create(builder => { builder.AddConsole(); });

}