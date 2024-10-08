using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public interface IUpdateRequisitesService
{
    Task<Result<Guid, Error>> UdpateRequisites(UpdateRequisitesRequest request, CancellationToken ct);
}