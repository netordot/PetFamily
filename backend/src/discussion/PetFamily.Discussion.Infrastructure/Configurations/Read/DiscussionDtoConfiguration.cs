using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos.Discussion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Infrastructure.Configurations.Read
{
    public class DiscussionDtoConfiguration : IEntityTypeConfiguration<DiscussionDto>
    {
        public void Configure(EntityTypeBuilder<DiscussionDto> builder)
        {
            builder.ToTable("discussions");

            builder.HasKey(x => x.Id);

            builder.Property(d => d.UserId)
                .HasColumnName("id");

            builder.Property(d => d.Status)
               .HasColumnName("status");

            builder.Property(d => d.AdminId)
              .HasColumnName("admin_id");

            builder.Property(d => d.RelationId)
              .HasColumnName("relation_id");

        }
    }
}
