using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Configurations.Read
{
    public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDto>
    {
        public void Configure(EntityTypeBuilder<BreedDto> builder)
        {
            builder.ToTable("breeds");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.SpeciesId)
                .HasColumnName("species_id");


        }
    }
}
