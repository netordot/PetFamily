using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain;
using PetFamily.Domain.Volunteer;
using PetFamily.Infrastructure.Interceptors;

namespace PetFamily.Infrastructure;


public class ApplicationDbContext(IConfiguration configuration,
    SoftDeleteInterceptor softDeleteInterceptor) : DbContext
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Species> Species => Set<Species>();
    
    private const string DATABASE = nameof(Database);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(softDeleteInterceptor);
    }
    
    private ILoggerFactory CreateLoggerFactory()
    => LoggerFactory.Create(builder => {builder.AddConsole(); });
} 