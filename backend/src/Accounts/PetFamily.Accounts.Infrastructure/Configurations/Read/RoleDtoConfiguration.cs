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
    public class RoleDtoConfiguration : IEntityTypeConfiguration<RoleDto>
    {
        public void Configure(EntityTypeBuilder<RoleDto> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);
        }
    }
}
