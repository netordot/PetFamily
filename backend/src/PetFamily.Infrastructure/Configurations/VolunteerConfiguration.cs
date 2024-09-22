using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

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
        
        builder.Property(v => v.Description)
            .HasMaxLength(Constants.MAX_DESCRIPTION_SIZE);
        
        builder.OwnsOne(p => p.Requisites, pbuilder =>
        {
            pbuilder.ToJson("volunteer_requisites");

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