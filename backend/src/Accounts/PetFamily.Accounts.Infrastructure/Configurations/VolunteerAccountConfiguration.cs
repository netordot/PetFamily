using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.AccountManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Configurations
{
    public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
    {
        public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
        {
            builder.ComplexProperty(v => v.FullName, nb =>
            {
                nb.Property(n => n.Name).HasColumnName("name");
                nb.Property(n => n.LastName).HasColumnName("last_name");
                nb.Property(n => n.MiddleName).HasColumnName("middle_name");
            });
        }
    }
}
