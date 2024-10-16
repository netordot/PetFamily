using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;
using PetFamily.Domain.Pet;
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
            .HasMaxLength(Constants.MAX_LONG_TEXT_SIZE);

        builder.Property(p => p.Name)
            .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);

        builder.OwnsOne(p => p.Requisites, pbuilder =>
        {
            pbuilder.ToJson("requisites");

            pbuilder.OwnsMany(pbuilder => pbuilder.Value, vb =>
            {
                vb.Property(v => v.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LONG_TEXT_SIZE);
                vb.Property(v => v.Title)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);
            });
        });

        //builder.OwnsOne(p => p.Photos, pbuilder =>
        //{
        //    pbuilder.OwnsMany(pbuilder => pbuilder.Photos);

        //});

        //builder.OwnsOne(p => p.Photos, phb =>
        //{
        //    phb.Property(ph => ph.Photos)
        //    .IsRequired();
        //});
            

        builder.ComplexProperty(v => v.PhoneNumber, eb =>
        {
            eb.Property(e => e.Number).IsRequired();
        });

        builder.ComplexProperty(v => v.Address, ab =>
        {
            ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);

            ab.Property(a => a.BuildingNumber)
                .IsRequired();

        });

        builder.ComplexProperty(p => p.SpeciesBreed, pb =>
        {
            pb.Property(sb => sb.BreedId)
                .IsRequired()
                .HasColumnName("BreedId");

            pb.Property(sb => sb.SpeciesId)
                .IsRequired()
                .HasConversion(
                    Id => Id.Value,
                    value => SpeciesId.Create(value))
                .HasColumnName("SpeciesId");

        });

        builder.Property<bool>("_isDeleted")
           .UsePropertyAccessMode(PropertyAccessMode.Field)
           .HasColumnName("deleted");
    }
}