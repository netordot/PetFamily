using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Infrastructure.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Infrastructure.Data
{
    public class ReadDbContext(IConfiguration configuration,
    SoftDeleteInterceptor softDeleteInterceptor) : DbContext, IReadDbContext
    {
        readonly ILoggerFactory _loggerFactory = new LoggerFactory();

        public DbSet<VolunteerDto> Volunteers => Set<VolunteerDto>();
        public DbSet<PetDto> Pets => Set<PetDto>();

        private const string DATABASE = nameof(Database);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Read") ?? false);

            modelBuilder.HasDefaultSchema("volunteers");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging().LogTo(Console.WriteLine);

            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)),
                x => x.MigrationsHistoryTable("__MyMigrationsHistory", "public"));
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.AddInterceptors(softDeleteInterceptor);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        

    }
}
