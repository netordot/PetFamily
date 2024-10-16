using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Species
{
    public class CreateSpeciesService
    {
        private readonly ISpeciesRepository _speciesRepository;

        public CreateSpeciesService(ISpeciesRepository repository)
        {
            _speciesRepository = repository;
        }

        public async Task<Result<Guid, Error>> Create(CreateSpeciesCommand request, CancellationToken cancellation)
        {
            var speciesToAdd = Domain.Species.Create(request.SpeciesName, null, request.id);

            if (speciesToAdd.IsFailure)
                return speciesToAdd.Error;

            var speciesResult = await _speciesRepository.Create(speciesToAdd.Value, cancellation);
            if (speciesResult.IsFailure)
                return speciesResult.Error;

            return speciesResult.Value;
        }

    }
}
