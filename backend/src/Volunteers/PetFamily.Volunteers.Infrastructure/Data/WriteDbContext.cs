using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Volunteers.Domain.AggregateRoot;
using PetFamily.Volunteers.Infrastructure.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Infrastructure.Data
{
    public class WriteDbContext(IConfiguration configuration,
     SoftDeleteInterceptor softDeleteInterceptor) : DbContext
    {
        readonly ILoggerFactory _loggerFactory = new LoggerFactory();

        public DbSet<Volunteer> Volunteers => Set<Volunteer>();

        private const string DATABASE = nameof(Database);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Write") ?? false);

            modelBuilder.HasDefaultSchema("volunteers");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.AddInterceptors(softDeleteInterceptor);
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging().LogTo(Console.WriteLine);

            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)),
                x => x.MigrationsHistoryTable("__MyMigrationsHistory", "public"));

            optionsBuilder.UseSnakeCaseNamingConvention();
        }

    }
}
