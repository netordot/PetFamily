using CSharpFunctionalExtensions;
using PetFamily.Domain;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken = default);
    Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Guid, Error>> Delete(Volunteer volunteer, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetVolunteerByPetId(PetId petId);


}