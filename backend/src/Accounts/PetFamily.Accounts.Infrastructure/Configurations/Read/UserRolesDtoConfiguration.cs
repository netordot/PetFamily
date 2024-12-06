using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Configurations.Read
{
    public class UserRolesDtoConfiguration : IEntityTypeConfiguration<UserRolesDto>
    {
        public void Configure(EntityTypeBuilder<UserRolesDto> builder)
        {
            builder.ToTable("user_roles");

            builder.HasKey(u => new { u.UserId, u.RoleId });
        }
    }
}
