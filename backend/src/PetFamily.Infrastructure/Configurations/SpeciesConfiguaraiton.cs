using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;

namespace PetFamily.Infrastructure.Configurations;

public class SpeciesConfiguaraiton :IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(
        
            Id => Id.Value,
            Value => SpeciesId.Create(Value));

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_SIZE);
    }
}