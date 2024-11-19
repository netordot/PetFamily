using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateMainInfo;

public interface IUpdateVolunteerService
{
    Task<Result<Guid, Error>> Update(UpdateVolunteerCommand request, CancellationToken cancellationToken);
}