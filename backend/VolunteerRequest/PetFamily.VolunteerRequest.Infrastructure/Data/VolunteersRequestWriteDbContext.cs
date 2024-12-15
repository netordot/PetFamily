using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSharpFunctionalExtensions.Result;

namespace PetFamily.VolunteerRequest.Infrastructure.Data
{
    public class VolunteersRequestWriteDbContext(IConfiguration configuration) : DbContext
    {
        private const string DATABASE = nameof(Database);
        public DbSet<VolunteerRequest.Domain.AggregateRoot.VolunteerRequest> VolunteerRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VolunteersRequestWriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Write") ?? false);

            modelBuilder.HasDefaultSchema("volunteer_requests");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)));
            optionsBuilder.UseSnakeCaseNamingConvention();
            //optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            optionsBuilder.EnableSensitiveDataLogging();
        }

        //private ILoggerFactory CreateLoggerFactory()
        //=> LoggerFactory.Create(builder => { builder.AddConsole(); });

    }
}
