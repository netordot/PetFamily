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

        builder.OwnsOne(v => v.Emails, vbuilder =>
        {
            vbuilder.ToJson("emails");
            vbuilder.OwnsMany(vbuilder => vbuilder.Mails, mbuilder =>
            {
                mbuilder.Property(m => m.Mail).IsRequired();
            });
        });

        builder.OwnsOne(v => v.Numbers, vbuilder =>
        {
            vbuilder.ToJson("phone_number");
            vbuilder.OwnsMany(vbuilder => vbuilder.Numbers, nb =>
            {
                nb.Property(n => n.Number).IsRequired();
            });

        });

        builder.ComplexProperty(v => v.Name, vbuilder =>
        {
            vbuilder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_SIZE);
            
            vbuilder.Property(v => v.MiddleName)
                .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_SIZE);
            vbuilder.Property(v => v.LastName)
                .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_SIZE);

        });
    }
}