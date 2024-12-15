using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Discussion.Domain.Entities;
using PetFamily.SharedKernel.Id;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Infrastructure.Configurations.Write
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("messages");

            builder.HasKey(x => x.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                    Id => Id.Value,
                    value => MessageId.Create(value));

            builder.Property(m => m.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            builder.Property(m => m.DiscussionId)
                .HasColumnName("discussion_id");

            builder.Property(m => m.Text)
                .IsRequired()
                .HasColumnName("text");

            builder.Property(m => m.IsEdited)
                .HasColumnName("is_edited");

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