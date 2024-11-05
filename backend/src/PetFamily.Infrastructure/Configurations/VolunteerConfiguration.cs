using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Application.Volunteers.SharedDtos;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Requisites;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.Details;
using PetFamily.Infrastructure.Extensions;

namespace PetFamily.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(
                Id => Id.Value,
                value => VolunteerId.Create(value));

        builder.Property(v => v.Experience)
            .IsRequired()
            .HasColumnName("experience");

        builder.Property(v => v.Description)
            .HasMaxLength(Constants.MAX_LONG_TEXT_SIZE);


        builder.Property(v => v.Requisites)
            .ValueObjectJsonConversion
            (requisite => new Application.Dtos.RequisiteDto(requisite.Title, requisite.Description),
                dto => Requisite.Create(dto.Title, dto.Description).Value)
            .HasColumnName("requisites");

        builder.Property(v => v.Socials)
            .ValueObjectJsonConversion(
            social => new SocialDto(social.Name, social.Link),
            dto => Social.Create(dto.Name, dto.Link).Value);


        builder.ComplexProperty(v => v.Email, eb => { eb.Property(e => e.Mail).IsRequired(); });

        builder.ComplexProperty(v => v.Number, eb => { eb.Property(e => e.Number).IsRequired(); });


        builder.ComplexProperty(v => v.Name, vbuilder =>
        {
            vbuilder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("name");

            vbuilder.Property(v => v.MiddleName)
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("middle_name");

            vbuilder.Property(v => v.LastName)
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("last_name");
        });

        builder.ComplexProperty(v => v.Address, ab =>
        {
            ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("city");

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("street");

            ab.Property(a => a.BuildingNumber)
                .IsRequired()
                .HasColumnName("building_number");
            ab.Property(a => a.CropsNumber)
                .HasColumnName("crops_number");
        });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("deleted");
    }
}