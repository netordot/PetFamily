using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Pet.Species;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Configurations.Read
{
    public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
    {
        public void Configure(EntityTypeBuilder<PetDto> builder)
        {
            builder.ToTable("pets");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.VolunteerId).IsRequired();


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

            builder.Property(p => p.DateOfBirth)
               .HasColumnName("birth_date")
               .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasConversion(
            src => src.Kind == DateTimeKind.Utc
            ? src
            : DateTime.SpecifyKind(src, DateTimeKind.Utc),

            dst => dst.Kind == DateTimeKind.Utc
            ? dst
            : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

            builder.Property(p => p.DateOfBirth)
                .HasConversion(
            src => src.Kind == DateTimeKind.Utc
            ? src
            : DateTime.SpecifyKind(src, DateTimeKind.Utc),

            dst => dst.Kind == DateTimeKind.Utc
            ? dst
            : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(v => v.PhoneNumber).IsRequired();


            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);

            builder.Property(a => a.BuildingNumber)
                .IsRequired();



            builder.Property(sb => sb.BreedId)
                .IsRequired()
                .HasColumnName("breed_id");

            builder.Property(sb => sb.SpeciesId)
                .IsRequired()
                .HasColumnName("species_id");


            builder.Property<bool>("_isDeleted")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasColumnName("deleted");

            builder.Property(s => s.Position)
            .HasColumnName("position")
            .IsRequired();

        }
    }
}
