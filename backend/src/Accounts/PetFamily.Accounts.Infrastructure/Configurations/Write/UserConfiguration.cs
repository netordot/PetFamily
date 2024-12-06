using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Application.AccountManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Configurations.Write
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(u => u.SocialNetworks)
                .HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<SocialNetwork>>(json, JsonSerializerOptions.Default)!);

            builder.Property(u => u.Email)
                .HasColumnName("email");

            builder
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<IdentityUserRole<Guid>>();

            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.AdminAccount)
                .WithOne(a => a.User)
                .HasForeignKey<AdminAccount>("user_id")
                .IsRequired(false);

            builder.HasOne(u => u.VolunteerAccount)
                .WithOne(a => a.User)
                .HasForeignKey<VolunteerAccount>("user_id")
                .IsRequired(false);

            builder.HasOne(u => u.ParticipantAccount)
                .WithOne(a => a.User)
                .HasForeignKey<ParticipantAccount>("user_id")
                .IsRequired(false);




        }
    }
}
