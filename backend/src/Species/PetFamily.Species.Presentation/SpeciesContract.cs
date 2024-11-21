using Microsoft.EntityFrameworkCore;
using PetFamily.Species.Application;
using PetFamily.Species.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Species.Presentation
{
    public class SpeciesContract : ISpeciesContract
    {
        private readonly IReadDbContext _context;

        public SpeciesContract(IReadDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SpeciesBreedExists(Guid speciesId, Guid breedId, CancellationToken cancellation)
        {
            var result = await _context.Breeds.AnyAsync(b => b.Id == breedId && b.SpeciesId == speciesId);

            return result;
        }
    }
}
