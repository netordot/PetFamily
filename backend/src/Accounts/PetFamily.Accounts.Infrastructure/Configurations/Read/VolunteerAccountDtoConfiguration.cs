using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos.Accounts;
using PetFamily.Core.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Configurations.Read
{
    public class VolunteerAccountDtoConfiguration : IEntityTypeConfiguration<VolunteerAccountDto>
    {
        public void Configure(EntityTypeBuilder<VolunteerAccountDto> builder)
        {
            builder.ToTable("volunteer_accounts");

            builder.Property(v => v.Requisites)
              .HasConversion(
           files => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
           json => JsonSerializer.Deserialize<IEnumerable<RequisiteDto>>(json, JsonSerializerOptions.Default)!)
             .HasColumnName("requisites");

            builder.Property(v => v.FirstName)
                .HasColumnName("first_name");

            builder.Property(v => v.MiddleName)
                .HasColumnName("middle_name");

            builder.Property(v => v.LastName)
                .HasColumnName("last_name");

            builder.HasKey(v => v.Id);


        }
    }
}
