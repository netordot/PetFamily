using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Dtos.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSharpFunctionalExtensions.Result;

namespace PetFamily.Accounts.Infrastructure.Data
{
    public class AccountsReadDbContext(IConfiguration configuration) : DbContext, IAccountsReadDbContext
    {
        readonly ILoggerFactory _loggerFactory = new LoggerFactory();

        private const string DATABASE = nameof(Database);
        public DbSet<UserDto> Users { get; set; }
        public DbSet<RoleDto> Roles { get; set; }
        //public DbSet<Permission> Permissions { get; set; }
        //public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ParticipantAccountDto> ParticipantAccounts { get; set; }
        public DbSet<VolunteerAccountDto> VolunteerAccounts { get; set; }
        public DbSet<AdminAccountDto> AdminAccounts { get; set; }
        //public DbSet<RefreshSession> RefreshSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountsReadDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Read") ?? false);

            modelBuilder.HasDefaultSchema("accounts");

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging().LogTo(Console.WriteLine);

            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(DATABASE)),
                x => x.MigrationsHistoryTable("__MyMigrationsHistory", "public"));
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
