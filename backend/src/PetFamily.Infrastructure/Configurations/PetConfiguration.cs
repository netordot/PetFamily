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
            pbuilder.ToJson("requisites");

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
            vbuilder.ToJson("phone_number");
            vbuilder.OwnsMany(vbuilder => vbuilder.Numbers, nb =>
            {
                nb.Property(n => n.Number).IsRequired();
            });

        });
        
        builder.ComplexProperty(v => v.Address, ab =>
        {
            ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_SIZE);

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_SIZE);

            ab.Property(a => a.BuildingNumber)
                .IsRequired();

        });

        builder.ComplexProperty(p => p.SpeciesBreed, pb =>
        {
            pb.Property(sb => sb.BreedId)
                .IsRequired();
            
            pb.Property(sb => sb.SpeciesId)
                .HasConversion(
                    Id => Id.Value,
                    value => SpeciesId.Create(value));
        });
    }
}