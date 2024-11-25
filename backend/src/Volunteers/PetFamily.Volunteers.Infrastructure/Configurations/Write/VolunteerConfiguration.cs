using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Core.Extensions;
using PetFamily.CoreCore.Dtos.PetManagement;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.AggregateRoot;

namespace PetFamily.Infrastructure.Configurations.Write;

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
            (requisite => new RequisiteDto(requisite.Title, requisite.Description),
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
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("name");

            vbuilder.Property(v => v.MiddleName)
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("middle_name");

            vbuilder.Property(v => v.LastName)
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("last_name");
        });

        builder.ComplexProperty(v => v.Address, ab =>
        {
            ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE)
                .HasColumnName("city");

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE)
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