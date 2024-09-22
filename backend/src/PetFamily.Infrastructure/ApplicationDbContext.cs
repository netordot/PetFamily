using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Infrastructure;


public class ApplicationDbContext(IConfiguration _configuration) : DbContext
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    
    private const string Database = nameof(Database);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString(Database));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }
    
    private ILoggerFactory CreateLoggerFactory()
    => LoggerFactory.Create(builder => {builder.AddConsole(); });
} 