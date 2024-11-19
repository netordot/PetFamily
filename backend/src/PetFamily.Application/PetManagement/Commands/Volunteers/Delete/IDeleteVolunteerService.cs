using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.Delete
{
    public interface IDeleteVolunteerService
    {
        Task<Result<Guid, Error>> Delete(DeleteVolunteerCommand request, CancellationToken cancellationToken);
    }
}