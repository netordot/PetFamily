using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel.Id;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Infrastructure.Configurations.Write
{
    public class DiscussionConfiguration : IEntityTypeConfiguration<Domain.AggregateRoot.Discussion>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregateRoot.Discussion> builder)
        {
            builder.ToTable("discussions");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                .HasConversion(id => id.Value,
                    value => DiscussionId.Create(value));

            builder.ComplexProperty(d => d.Users, db =>
            {
                db.Property(u => u.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                db.Property(u => u.AdminId)
                    .HasColumnName("admin_id")
                    .IsRequired();
            });

            builder.HasMany(d => d.Messages)
                .WithOne()
                .HasForeignKey(m => m.DiscussionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}