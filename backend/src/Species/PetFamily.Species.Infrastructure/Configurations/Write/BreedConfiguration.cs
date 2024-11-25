using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Id;
using PetFamily.Species.Domain.Entities;

namespace PetFamily.Species.Infrastructure.Configurations.Write;

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
            .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);

        builder.Property(b => b.SpeciesId)
            .IsRequired()
            .HasColumnName("species_id")
            .HasConversion(

            Id => Id.Value,
            Value => SpeciesId.Create(Value));

    }
}