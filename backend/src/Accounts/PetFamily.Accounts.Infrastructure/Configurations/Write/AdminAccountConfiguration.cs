using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.AccountManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Configurations.Write
{
    public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
    {
        public void Configure(EntityTypeBuilder<AdminAccount> builder)
        {
            builder.ToTable("admin_accounts");

            builder.ComplexProperty(a => a.FullName, nb =>
            {
                nb.Property(n => n.Name).HasColumnName("name");
                nb.Property(n => n.LastName).HasColumnName("last_name");
                nb.Property(n => n.MiddleName).HasColumnName("middle_name");
            });

        }
    }
}
