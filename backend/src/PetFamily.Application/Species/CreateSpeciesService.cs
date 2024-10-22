using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
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
        private readonly IUnitOfWork _UnitOfWork;

        public CreateSpeciesService(ISpeciesRepository repository, IUnitOfWork unitOfWork)
        {
            _speciesRepository = repository;
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, Error>> Create(CreateSpeciesCommand request, CancellationToken cancellation)
        {
            var speciesToAdd = Domain.Pet.Species.Species.Create(request.SpeciesName, null, request.id);

            if (speciesToAdd.IsFailure)
                return speciesToAdd.Error;

            var speciesResult = await _speciesRepository.Create(speciesToAdd.Value, cancellation);
            if (speciesResult.IsFailure)
                return speciesResult.Error;
            await _UnitOfWork.SaveChanges(cancellation);

            return speciesResult.Value;
        }

    }
}
