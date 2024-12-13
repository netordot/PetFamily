using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Core.Dtos.VolunteerRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Infrastructure.Configurations.Read
{
    public class VolunteerRequestDtoConfiguration : IEntityTypeConfiguration<VolunteerRequestDto>
    {
        public void Configure(EntityTypeBuilder<VolunteerRequestDto> builder)
        {
            builder.ToTable("volunteer_requests");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.AdminId)
                .HasColumnName("admin_id");

            builder.Property(v => v.DiscussionId)
               .HasColumnName("discussion_id");

            builder.Property(v => v.UserId)
                .HasColumnName("user_id");

            builder.Property(v => v.Status)
                .HasColumnName("status");

            builder.Property(v => v.FirstName)
                .HasColumnName("first_name");

            builder.Property(v => v.MiddleName)
               .HasColumnName("middle_name");

            builder.Property(v => v.LastName)
                .HasColumnName("last_name");

            builder.Property(v => v.Email)
                .HasColumnName("email");

            builder.Property(v => v.Description)
                .HasColumnName("description");

            builder.Property(v => v.Experience)
                .HasColumnName("experience");

            builder.Property(v => v.PhoneNumber)
                .HasColumnName("phone_number");

            builder.Property(v => v.RejectionComment)
                .HasColumnName("rejection_comment");

            builder.Property(v => v.Requisites)
                .HasConversion(
           files => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
           json => JsonSerializer.Deserialize<List<RequisiteDto>>(json, JsonSerializerOptions.Default)!);

            builder.Property(p => p.CreatedAt)
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
