using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;
using PetFamily.Domain.Pet.Breed;
using PetFamily.Domain.Pet.Species;

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
            .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);

        builder.Property(b => b.SpeciesId)
            .IsRequired()
            .HasColumnName("species_id")
            .HasConversion(

            Id => Id.Value,
            Value => SpeciesId.Create(Value));

    }
}