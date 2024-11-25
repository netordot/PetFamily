using CSharpFunctionalExtensions;
using PetFamily.Domain;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared.Errors;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Application.Species
{
    public interface ISpeciesRepository
    {
        Task<Result<Guid, Error>> Create(Domain.Pet.Species.Species species, CancellationToken cancellationToken);
        Task<Result<Domain.Pet.Species.Species, Error>> GetById(Guid SpeciesId, CancellationToken cancellation);
        Task<Result<Guid, Error>> Save(Domain.Pet.Species.Species species, CancellationToken cancellationToken = default);
        Task<Result<SpeciesBreed, Error>> GetSpeciesBreedByNames(string speciesName, string breedname,
            CancellationToken cancellation);
        Task<Result<Guid, Error>> Delete(Guid Id, CancellationToken cancellationToken = default);
    }
}