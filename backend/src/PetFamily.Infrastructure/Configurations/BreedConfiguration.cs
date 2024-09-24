using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;

namespace PetFamily.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        
        builder.HasKey(x => x.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                Id => Id.Value,
                value => BreedId.Create(value));
        
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_SIZE);
        
    }
}