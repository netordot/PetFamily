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

            builder.Property(v => v.Name)
                .HasColumnName("name");
            
            builder.Property(v => v.MiddleName)
                .HasColumnName("middle_name"); 
            
            builder.Property(v => v.LastName)
                .HasColumnName("last_name"); 
            
            builder.Property(v => v.Street)
                .HasColumnName("street");
            
            builder.Property(v => v.BuildingNumber)
                .HasColumnName("building_number"); 
            
            builder.Property(v => v.CorpsNumber)
                .HasColumnName("crops_number"); 
                
            builder.Property(v => v.Email)
                .HasColumnName("email_mail");

            builder.Property(v => v.Number)
                .HasColumnName("number_number");
            
            builder.Property(v => v.Description)
                .HasColumnName("description");

            builder.Property<bool>("_isDeleted")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasColumnName("deleted");


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
