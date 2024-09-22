using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description)
            .HasMaxLength(Constants.MAX_DESCRIPTION_SIZE);
        
        builder.Property(p =>p.Name)
            .HasMaxLength(Constants.MAX_TITLE_SIZE);

        builder.OwnsOne(p => p.Requisites, pbuilder =>
        {
            pbuilder.ToJson();

            pbuilder.OwnsMany(pbuilder => pbuilder.Value, vb =>
            {
                    vb.Property(v => v.Description)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_DESCRIPTION_SIZE);
                    vb.Property(v => v.Title)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_TITLE_SIZE);
            });
        });


    }
}