using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Dtos.Accounts;
using PetFamily.CoreCore.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Configurations.Read
{
    public class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
    {
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.SocialNewroks)
                .HasConversion(
           files => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
           json => JsonSerializer.Deserialize<IEnumerable<SocialDto>>(json, JsonSerializerOptions.Default)!);

            builder.HasOne(u => u.ParticipantAccount)
                .WithOne()
                .HasForeignKey<ParticipantAccountDto>(a => a.UserId);

            builder.HasOne(u => u.VolunteerAccount)
                .WithOne()
                .HasForeignKey<VolunteerAccountDto>(a => a.UserId);

            builder.HasOne(u => u.AdminAccount)
                .WithOne()
                .HasForeignKey<AdminAccountDto>(a => a.UserId);

            builder.HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity<UserRolesDto>(
                    ur => ur.HasOne(u => u.Role).WithMany().HasForeignKey(ur => ur.RoleId),
                    ur => ur.HasOne(u => u.User).WithMany().HasForeignKey(ur => ur.UserId)
                );


                
        }
    }
}
