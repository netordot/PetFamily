using CSharpFunctionalExtensions;
using PetFamily.Domain;
using PetFamily.Domain.Pet.Breed;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Pet.Species;

namespace PetFamily.Application.Species.AddBreeds
{
    public class AddBreedService
    {
        private readonly ISpeciesRepository _speciesRepository;

        public AddBreedService(ISpeciesRepository repository)
        {
            _speciesRepository = repository;
        }

        public async Task<Result<Guid, Error>> AddBreed(AddBreedCommand command, CancellationToken cancellation)
        {
            var breedId = BreedId.NewBreedId;
            var breed = Breed.Create(command.name, breedId, SpeciesId.Create(command.id));
            if (breed.IsFailure)
                return breed.Error;

            var species = await _speciesRepository.GetById(command.id, cancellation);
            if (species.IsFailure)
                return species.Error;

            species.Value.AddBreed(breed.Value);

            await _speciesRepository.Save(species.Value, cancellation);

            return species.Value.Id.Value;

        }
    }
}
