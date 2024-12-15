using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.VolunteerRequest.Application
{
    public interface IVolunteerRequestRepository
    {
        Task Add(Domain.AggregateRoot.VolunteerRequest volunteerRequest, CancellationToken cancellationToken);
        Task<Result<Domain.AggregateRoot.VolunteerRequest, Error>> GetById(Guid requestId, CancellationToken cancellation);
        Task<Result<Domain.AggregateRoot.VolunteerRequest, Error>> GetByUserId(Guid userId, CancellationToken cancellation);
    }
}