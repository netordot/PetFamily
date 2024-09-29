namespace PetFamily.Application.Volunteers.CreateVolunteer;

public interface ICreateVolunteerService
{
    Task<CSharpFunctionalExtensions.Result<Guid, string>> Create(CreateVolunteerRequest createVolunteerRequest, CancellationToken ct);
}