using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Core.Dtos.VolunteerRequest;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Domain.AggregateRoot;

namespace PetFamily.VolunteerRequest.Infrastructure.Data;

public class VolunteersRequestReadDbContext(IConfiguration configuration) : DbContext, IVolunteersRequestReadDbContext
{
    private const string DATABASE = nameof(Database);
    public DbSet<VolunteerRequestDto> VolunteerRequests { get; set; }
    public DbSet<UserRestrictionDto> UserRestrictions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VolunteersRequestReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);

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
