using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos.VolunteerRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Infrastructure.Configurations.Read
{
    public class UserRestrictionDtoConfiguration : IEntityTypeConfiguration<UserRestrictionDto>
    {
        public void Configure(EntityTypeBuilder<UserRestrictionDto> builder)
        {
            builder.ToTable("user_restrictions");   
        }
    }
}
