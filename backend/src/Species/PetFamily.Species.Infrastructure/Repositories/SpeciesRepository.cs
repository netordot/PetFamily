using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.Other;
using PetFamily.Species.Infrastructure.Data;
using PetFamily.Species.Application;

namespace PetFamily.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly WriteDbContext _context;
        private ILogger<SpeciesRepository> _logger;

        public SpeciesRepository(WriteDbContext applicationDbContext, ILogger<SpeciesRepository> logger)
        {
            _context = applicationDbContext;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Create(Species.Domain.AggregateRoot.Species species, CancellationToken cancellationToken)
        {
            var existSpecies = await GetByName(species.Name, cancellationToken);
            if (existSpecies.IsSuccess)
            {
                return Error.Conflict("already.exists", "species");
            }
            await _context.Species.AddAsync(species);

            return species.Id.Value;
        }

        public async Task<Result<Species.Domain.AggregateRoot.Species, Error>> GetById(Guid SpeciesId, CancellationToken cancellation)
        {
            var speciesResult = await _context.Species
                 .Include(s => s.Breeds)
                .FirstOrDefaultAsync(s => s.Id == SpeciesId, cancellation);

            if (speciesResult == null)
                return Errors.General.NotFound(SpeciesId);

            return speciesResult;
        }

        // в дальнейшем рассмотреть еще проверку через ToLower, чтобы точно никак не повторялось
        private async Task<Result<Species.Domain.AggregateRoot.Species, Error>> GetByName(string Name, CancellationToken cancellation)
        {
            var speciesResult = await _context.Species
                 .Include(s => s.Breeds)
                .FirstOrDefaultAsync(s => s.Name == Name, cancellation);

            if (speciesResult == null)
                return Errors.General.NotFound();

            return speciesResult;
        }

        public async Task<Result<Guid, Error>> Save(Species.Domain.AggregateRoot.Species species, CancellationToken cancellationToken = default)
        {
            _context.Species.Attach(species);

            return species.Id.Value;
        }

        public async Task<Result<Guid, Error>> Delete(Guid Id, CancellationToken cancellationToken = default)
        {
            var speciesToDelete = await GetById(Id, cancellationToken);
            _context.Species.Remove(speciesToDelete.Value);

            return Id;
        }

        // этот метод заменить тем, что будем тянуть через readDbContext
        //public async Task<Result<SpeciesBreed, Error>> GetSpeciesBreedByNames(string speciesName, string breedname,
        //    CancellationToken cancellation)
        //{
        //    var species = await GetByName(speciesName, cancellation);
        //    if (species.IsFailure)
        //        return Errors.General.NotFound();

        //    var breedList = species.Value.Breeds;
        //    if (breedList == null)
        //        return Errors.General.NotFound();

        //    var result = breedList.FirstOrDefault(b => b.Name == breedname);
        //    if (result == null)
        //        return Errors.General.NotFound();

        //    return new SpeciesBreed(species.Value.Id, result.Id.Value);
        //}
    }
}
