using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Pet.Species;
using PetFamily.Domain.Volunteer;
using PetFamily.SharedKernel.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Configurations.Read
{
    public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
    {
        public void Configure(EntityTypeBuilder<PetDto> builder)
        {
            builder.ToTable("pets");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.VolunteerId)
                .HasColumnName("volunteer_id")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasMaxLength(Constants.MAX_LONG_TEXT_SIZE);

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);

            builder.Property(p => p.Color)
                .HasColumnName("color");

            builder.Property(p => p.HealthCondition)
                .HasColumnName("health_condition");
            
            builder.Property(p => p.Status)
                .HasColumnName("status"); 
            
            builder.Property(p => p.Height)
                .HasColumnName("height"); 
            
            builder.Property(p => p.Weight)
                .HasColumnName("weight"); 
            
            builder.Property(p => p.IsCastrated)
                .HasColumnName("is_castrated"); 
            
            builder.Property(p => p.IsVaccinated)
                .HasColumnName("is_vaccinated"); 
            
            builder.Property(p => p.DateOfBirth)
                .HasColumnName("birth_date"); 
            
            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_date"); 
            
            builder.Property(p => p.City)
                .HasColumnName("address_city");
            
            builder.Property(p => p.Street)
                .HasColumnName("address_street");
            
            builder.Property(p => p.PhoneNumber)
                .HasColumnName("phone_number_number");
            
            builder.Property(p => p.SpeciesId)
                .HasColumnName("species_id");

            builder.Property(p => p.BreedId)
                .HasColumnName("breed_id");
            
            builder.Property(p => p.City)
                .HasColumnName("address_city");

            builder.Property(p => p.Photos)
                   .HasConversion(
           files => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
           json => JsonSerializer.Deserialize<IEnumerable<FileDto>>(json, JsonSerializerOptions.Default)!);
            
            builder.Property(p => p.Requisites)
                .HasConversion(
                    files => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                   json => JsonSerializer.Deserialize<IEnumerable<RequisiteDto>>(json, JsonSerializerOptions.Default)!); 

            builder.Property(p => p.CreatedAt)
                .HasConversion(
            src => src.Kind == DateTimeKind.Utc
            ? src
            : DateTime.SpecifyKind(src, DateTimeKind.Utc),

            dst => dst.Kind == DateTimeKind.Utc
            ? dst
            : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

            builder.Property(p => p.DateOfBirth)
                .HasConversion(
            src => src.Kind == DateTimeKind.Utc
            ? src
            : DateTime.SpecifyKind(src, DateTimeKind.Utc),

            dst => dst.Kind == DateTimeKind.Utc
            ? dst
            : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(v => v.PhoneNumber).IsRequired();

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);

            builder.Property(a => a.BuildingNumber)
                .IsRequired()
                .HasColumnName("address_building_number");

            builder.Property<bool>("_isDeleted")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasColumnName("deleted");

        }
    }
}
