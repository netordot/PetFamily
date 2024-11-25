using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Species.Application
{
    public interface IReadDbContext
    {
        public DbSet<SpeciesDto> Species { get; }
        public DbSet<BreedDto> Breeds { get; }
    }

}
