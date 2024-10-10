using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Volunteers.UpdateVolunteer;

public interface IUpdateVolunteerService
{
    Task<Result<Guid, Error>> Update(UpdateVolunteerRequest request, CancellationToken cancellationToken);
}