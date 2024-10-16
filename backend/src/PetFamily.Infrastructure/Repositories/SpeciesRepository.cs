using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Species;
using PetFamily.Domain;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly ApplicationDbContext _context;
        private ILogger<SpeciesRepository> _logger;

        public SpeciesRepository(ApplicationDbContext applicationDbContext, ILogger<SpeciesRepository> logger)
        {
            _context = applicationDbContext;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Create(Species species, CancellationToken cancellationToken)
        {
            var existSpecies = await GetByName(species.Name, cancellationToken);
            if (existSpecies.IsSuccess)
            {
                return Error.Conflict("already.exists", "species");
            }
            await _context.Species.AddAsync(species);
            await _context.SaveChangesAsync();

            return species.Id.Value;
        }

        public async Task<Result<Species, Error>> GetById(Guid SpeciesId, CancellationToken cancellation)
        {
            var speciesResult = await _context.Species
                // .Include(s => s.Breeds)
                .FirstOrDefaultAsync(s => s.Id == SpeciesId, cancellation);

            if (speciesResult == null)
                return Errors.General.NotFound(SpeciesId);

            return speciesResult;
        }

        // в дальнейшем рассмотреть еще проверку через ToLower, чтобы точно никак не повторялось
        public async Task<Result<Species,Error>> GetByName(string Name, CancellationToken cancellation)
        {
            var speciesResult = await _context.Species
                // .Include(s => s.Breeds)
                .FirstOrDefaultAsync(s => s.Name == Name, cancellation);

            if (speciesResult == null)
                return Errors.General.NotFound();

            return speciesResult;
        }

        public async Task<Result<Guid, Error>> Save(Species species, CancellationToken cancellationToken = default)
        {
            _context.Species.Attach(species);
            await _context.SaveChangesAsync(cancellationToken);

            return species.Id.Value;
        }

    }
}
