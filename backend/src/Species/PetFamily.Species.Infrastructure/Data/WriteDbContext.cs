using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Species.Domain.AggregateRoot;
using System.Data;

namespace PetFamily.Species.Infrastructure.Data;


public class WriteDbContext(IConfiguration configuration) : DbContext
{
    // the line where ex occurs
    public DbSet<Species.Domain.AggregateRoot.Species> Species => Set<Species.Domain.AggregateRoot.Species>();

    private const string DATABASE = nameof(Database);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }

    private ILoggerFactory CreateLoggerFactory()
    => LoggerFactory.Create(builder => { builder.AddConsole(); });

}