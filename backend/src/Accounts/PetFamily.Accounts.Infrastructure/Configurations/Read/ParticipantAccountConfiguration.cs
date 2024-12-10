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
    public class ParticipantAccountConfiguration : IEntityTypeConfiguration<ParticipantAccountDto>
    {
        public void Configure(EntityTypeBuilder<ParticipantAccountDto> builder)
        {
            builder.ToTable("participant_accounts");

            builder.HasKey(x => x.Id);

            builder.Property(n => n.FristName).HasColumnName("first_name");
            builder.Property(n => n.LastName).HasColumnName("last_name");
            builder.Property(n => n.MiddleName).HasColumnName("middle_name");


        }
    }
}
