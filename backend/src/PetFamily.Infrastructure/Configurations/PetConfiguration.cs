using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasConversion(
                Id => Id.Value,
                value => PetId.Create(value));

        builder.Property(p => p.Description)
            .HasMaxLength(Constants.MAX_DESCRIPTION_SIZE);
        
        builder.Property(p =>p.Name)
            .HasMaxLength(Constants.MAX_TITLE_SIZE);

        builder.OwnsOne(p => p.Requisites, pbuilder =>
        {
            pbuilder.ToJson("pet_requisites");

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
        
        builder.OwnsOne(v => v.PhoneNumbers, vbuilder =>
        {
            vbuilder.ToJson("pet_phone_number");
            vbuilder.OwnsMany(vbuilder => vbuilder.Numbers, nb =>
            {
                nb.Property(n => n.Number).IsRequired();
            });

        });


    }
}