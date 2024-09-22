using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;

namespace PetFamily.Infrastructure.Configurations;

public class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
{
    public void Configure(EntityTypeBuilder<PetPhoto> builder)
    {
        builder.ToTable("PetPhotos");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(pp=> pp.Path)
            .IsRequired();
        
        builder.Property(pp =>pp.isMain)
            .IsRequired();
    }
}