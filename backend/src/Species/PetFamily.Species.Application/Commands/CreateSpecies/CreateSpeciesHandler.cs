using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Species.Application.Commands.CreateSpecies
{
    public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSpeciesHandler(ISpeciesRepository repository,
            [FromKeyedServices(ModuleNames.Species)] IUnitOfWork unitOfWork)
        {
            _speciesRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ErrorList>> Handle(CreateSpeciesCommand request, CancellationToken cancellation)
        {
            var speciesToAdd = Domain.AggregateRoot.Species.Create(request.SpeciesName, null, request.id);

            if (speciesToAdd.IsFailure)
                return speciesToAdd.Error.ToErrorList();

            var speciesResult = await _speciesRepository.Create(speciesToAdd.Value, cancellation);
            if (speciesResult.IsFailure)
                return speciesResult.Error.ToErrorList();
            await _unitOfWork.SaveChanges(cancellation);

            return speciesResult.Value;
        }

    }
}
