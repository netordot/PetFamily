using CSharpFunctionalExtensions;
using PetFamily.Domain;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Volunteer;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Application.PetManagement.Commands.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken = default);
    Guid Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken);

}