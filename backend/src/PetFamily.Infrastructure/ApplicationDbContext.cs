using Microsoft.EntityFrameworkCore;
using PetFamily.Domain;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Volunteer> Volunteers { get; set; }
    public DbSet<Pet> Pets { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("");
    }
}