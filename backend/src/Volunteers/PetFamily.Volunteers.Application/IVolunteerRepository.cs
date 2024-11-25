using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.AggregateRoot;

namespace PetFamily.Volunteers.Application;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken = default);
    Guid Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken);

}