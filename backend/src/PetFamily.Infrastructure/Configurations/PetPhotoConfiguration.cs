using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;

namespace PetFamily.Infrastructure.Configurations;

public class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
{
    public void Configure(EntityTypeBuilder<PetPhoto> builder)
    {
        builder.ToTable("pet_photos");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                Id => Id.Value,
                value => PetPhotoId.Create(value));
        
        builder.Property(pp=> pp.Path)
            .IsRequired();
        
        builder.Property(pp =>pp.IsMain)
            .IsRequired();
    }
}