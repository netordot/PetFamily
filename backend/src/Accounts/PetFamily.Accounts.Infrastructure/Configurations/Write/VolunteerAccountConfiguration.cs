using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Core.Extensions;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.CoreCore.Dtos.PetManagement;
using System.Text.Json;

namespace PetFamily.Accounts.Infrastructure.Configurations.Write;

public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.ToTable("volunteer_accounts");
        builder.ComplexProperty(v => v.FullName, nb =>
        {
            nb.Property(n => n.Name).HasColumnName("first_name");
            nb.Property(n => n.LastName).HasColumnName("last_name");
            nb.Property(n => n.MiddleName).HasColumnName("middle_name");
        });

        builder.Property(v => v.Requisites)
            .HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<Requisite>>(json, JsonSerializerOptions.Default)!)
            .HasColumnName("requisites");

        builder.ComplexProperty(v => v.FullName, nb =>
        {
            nb.Property(n => n.Name)
            .HasColumnName("first_name");

            nb.Property(n => n.MiddleName)
            .HasColumnName("middle_name");

            nb.Property(n => n.LastName)
            .HasColumnName("last_name");
        });

    }
}
