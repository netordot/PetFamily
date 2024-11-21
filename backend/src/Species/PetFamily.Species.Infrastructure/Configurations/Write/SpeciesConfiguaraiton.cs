using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Id;

namespace PetFamily.Infrastructure.Configurations.Write;

public class SpeciesConfiguaraiton :IEntityTypeConfiguration<Species.Domain.AggregateRoot.Species>
{
    public void Configure(EntityTypeBuilder<Species.Domain.AggregateRoot.Species> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(
        
            Id => Id.Value,
            Value => SpeciesId.Create(Value));

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(s => s.SpeciesId);
    }
}