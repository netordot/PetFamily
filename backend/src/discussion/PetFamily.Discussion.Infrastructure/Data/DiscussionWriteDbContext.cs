using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetFamily.Discussion.Application;
using PetFamily.SharedKernel.Id;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Infrastructure.Data;

public class DiscussionWriteDbContext : DbContext, IDiscussionWriteDbContext
{
    private readonly IConfiguration _configuration;

    public DiscussionWriteDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const string DATABASE = nameof(Database);

    public DbSet<Discussion.Domain.AggregateRoot.Discussion> Discussions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString(nameof(Database)));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiscussionWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);

        modelBuilder.HasDefaultSchema("discussions");
    }
}