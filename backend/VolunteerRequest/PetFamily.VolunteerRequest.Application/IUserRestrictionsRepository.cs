using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.AggregateRoot;

namespace PetFamily.VolunteerRequest.Application
{
    public interface IUserRestrictionsRepository
    {
        Task<Result<Guid, Error>> Add(UserRestriction userRestriction, CancellationToken cancellation);
        Task<Result<UserRestriction, Error>> GetByDiscussionId(Guid id, CancellationToken cancellation);
    }
}