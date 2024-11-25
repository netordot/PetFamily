using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Species.Application
{
    public interface ISpeciesRepository
    {
        Task<Result<Guid, Error>> Create(Species.Domain.AggregateRoot.Species species, CancellationToken cancellationToken);
        Task<Result<Species.Domain.AggregateRoot.Species, Error>> GetById(Guid SpeciesId, CancellationToken cancellation);
        Task<Result<Guid, Error>> Save(Species.Domain.AggregateRoot.Species species, CancellationToken cancellationToken = default);
        Task<Result<Guid, Error>> Delete(Guid Id, CancellationToken cancellationToken = default);
    }
}