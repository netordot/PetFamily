using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Configurations.Read
{
    public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
    {
        public void Configure(EntityTypeBuilder<VolunteerDto> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey(v => v.Id);


            builder.Property(v => v.Requisites)
                   .HasConversion(
           files => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
           json => JsonSerializer.Deserialize<IEnumerable<RequisiteDto>>(json, JsonSerializerOptions.Default)!);


            builder.Property(v => v.Socials)
                   .HasConversion(
           files => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
           json => JsonSerializer.Deserialize<IEnumerable<SocialDto>>(json, JsonSerializerOptions.Default)!);


        }
    }
}
