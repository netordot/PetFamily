using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel.Id;
using PetFamily.VolunteerRequest.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Infrastructure.Configurations.Write
{
    public class UserRestrictionConfiguration : IEntityTypeConfiguration<UserRestriction>
    {
        public void Configure(EntityTypeBuilder<UserRestriction> builder)
        {
            builder.ToTable("user_restrictions");

            builder.HasKey(x => x.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                    Id => Id.Value,
                    value => UserRestrictionId.Create(value));

            builder.Property(v => v.BannedUntil)
           .HasConversion(
                src => src.Kind == DateTimeKind.Utc
                ? src
                : DateTime.SpecifyKind(src, DateTimeKind.Utc),

               dst => dst.Kind == DateTimeKind.Utc
               ? dst
               : DateTime.SpecifyKind(dst, DateTimeKind.Utc));
        }
    }
}
