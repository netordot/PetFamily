using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Infrastructure.Configurations.Write
{
    public class VolunteerRequestConfiguration : IEntityTypeConfiguration<VolunteerRequest.Domain.AggregateRoot.VolunteerRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VolunteerRequest.Domain.AggregateRoot.VolunteerRequest> builder)
        {
            builder.ToTable("volunteer_requests");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                    Id => Id.Value,
                    value => VolunteerRequestId.Create(value));

            builder.Property(v => v.AdminId)
                .HasColumnName("admin_id");

            builder.Property(v => v.UserId)
                .HasColumnName("user_id");

            builder.Property(v => v.DiscussionId)
                .HasColumnName("discussion_id");

            builder.Property(v => v.Status)
              .HasColumnName("status");

            builder.Property(v => v.RejectionComment)
                .HasColumnName("rejection_comment");

            builder.ComplexProperty(v => v.VolunteerInfo, vb =>
            {
                vb.Property(vb => vb.Description)
                .HasColumnName("description");

                vb.ComplexProperty(vb => vb.FullName, fb =>
                {
                    fb.Property(vb => vb.Name)
                    .HasColumnName("first_name");

                    fb.Property(vb => vb.MiddleName)
                    .HasColumnName("middle_name");

                    fb.Property(vb => vb.LastName)
                    .HasColumnName("last_name");
                });

                vb.Property(u => u.Requisites)
                .HasColumnName("requisites")
               .HasConversion(
               u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
               json => JsonSerializer.Deserialize<List<Requisite>>(json, JsonSerializerOptions.Default)!);

                vb.ComplexProperty(v => v.Email, eb =>
                { eb.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("email"); 
                });

                vb.ComplexProperty(v => v.PhoneNumber, eb =>
                { eb.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("phone_number"); 
                });
            });

            builder.Property(v => v.CreatedAt)
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
