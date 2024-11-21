using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Presentation
{
    public class VolunteersContract : IVolunteersContract
    {
        private IReadDbContext _context;

        public VolunteersContract(IReadDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsBreedInUse(Guid breedId, CancellationToken cancellation)
        {
            return await _context.Pets.AnyAsync(p => p.BreedId == breedId);
        }

        public async Task<bool> IsSpeciesInUse(Guid speciesId,CancellationToken cancellation)
        {
            return await _context.Pets.AnyAsync(p => p.SpeciesId == speciesId);
        }
    }
}
