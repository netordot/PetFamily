using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Configurations.Write
{
    public class ParticipantAccountConfiguration : IEntityTypeConfiguration<ParticipantAccount>
    {
        public void Configure(EntityTypeBuilder<ParticipantAccount> builder)
        {
            builder.ComplexProperty(p => p.FullName, nb =>
            {
                nb.Property(n => n.Name).HasColumnName("first_name");
                nb.Property(n => n.LastName).HasColumnName("last_name");
                nb.Property(n => n.MiddleName).HasColumnName("middle_name");
            });
        }
    }
}
