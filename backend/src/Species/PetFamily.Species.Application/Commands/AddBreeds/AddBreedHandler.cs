using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.Id;
using PetFamily.Core.Providers;
using PetFamily.Species.Application.Commands.AddBreeds;
using PetFamily.Species.Application;
using PetFamily.Species.Domain.Entities;

namespace PetFamily.Application.Species.AddBreeds
{
    public class AddBreedHandler : ICommandHandler<Guid, AddBreedCommand>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddBreedHandler(ISpeciesRepository repository, IUnitOfWork unitOfWork)
        {
            _speciesRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ErrorList>> Handle(AddBreedCommand command, CancellationToken cancellation)
        {
            var breedId = BreedId.NewBreedId;
            var breed = Breed.Create(command.name, breedId, SpeciesId.Create(command.id));
            if (breed.IsFailure)
                return breed.Error.ToErrorList();

            var species = await _speciesRepository.GetById(command.id, cancellation);
            if (species.IsFailure)
                return species.Error.ToErrorList();

            species.Value.AddBreed(breed.Value);

            await _unitOfWork.SaveChanges(cancellation);

            return species.Value.Id.Value;

        }
    }
}
