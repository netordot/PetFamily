using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Volunteers.Create;

public interface ICreateVolunteerService
{
    Task<CSharpFunctionalExtensions.Result<Guid, ErrorList>> Create(CreateVolunteerCommand createVolunteerRequest,
        CancellationToken ct);
}