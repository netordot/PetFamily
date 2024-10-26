using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Volunteers.Delete
{
    public interface IDeleteVolunteerService
    {
        Task<Result<Guid, Error>> Delete(DeleteVolunteerCommand request, CancellationToken cancellationToken);
    }
}