using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Discussion.Application
{
    public interface IDiscussionsRepository
    {
        Task Add(Domain.AggregateRoot.Discussion discussion, CancellationToken cancellation);
        Task<Result<Domain.AggregateRoot.Discussion, Error>> GetById(Guid Id, CancellationToken cancellation);
    }
}