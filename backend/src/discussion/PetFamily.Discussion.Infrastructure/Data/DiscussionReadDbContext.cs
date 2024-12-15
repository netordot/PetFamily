using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetFamily.Core.Dtos.Discussion;
using PetFamily.Discussion.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Infrastructure.Data
{
    public class DiscussionReadDbContext(IConfiguration configuration) : DbContext, IDiscussionReadDbContext
    {
        private const string DATABASE = nameof(Database);
        public DbSet<DiscussionDto> Discussions { get; set; }
        public DbSet<MessageDto> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)));
            optionsBuilder.UseSnakeCaseNamingConvention();
            //optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiscussionReadDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Read") ?? false);

            modelBuilder.HasDefaultSchema("discussions");

        }

        //private ILoggerFactory CreateLoggerFactory()
        //=> LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
