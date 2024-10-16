using CSharpFunctionalExtensions;
using PetFamily.Domain;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Species
{
    public interface ISpeciesRepository
    {
        Task<Result<Guid, Error>> Create(Domain.Species species, CancellationToken cancellationToken);
        Task<Result<Domain.Species, Error>> GetById(Guid SpeciesId, CancellationToken cancellation);
        Task<Result<Guid, Error>> Save(Domain.Species species, CancellationToken cancellationToken = default);
    }
}