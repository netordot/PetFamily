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
    public class MessageDtoConfiguration : IEntityTypeConfiguration<MessageDto>
    {
        public void Configure(EntityTypeBuilder<MessageDto> builder)
        {
            builder.ToTable("messages");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.UserId)
                .IsRequired()
                .HasColumnName("id");

            builder.Property(m => m.DiscussionId)
                .IsRequired()
                .HasColumnName("discussion_id");

            builder.Property(m => m.Text)
                .IsRequired()
                .HasColumnName("text");

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
