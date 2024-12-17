using CSharpFunctionalExtensions;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application
{
    public interface IVolunteerAccountManager
    {
        Task Add(VolunteerAccount volunteerAccount, CancellationToken cancellation);
        Task<Result<VolunteerAccount, Error>> GetById(Guid id, CancellationToken cancellation);
        Task<Result<VolunteerAccount, Error>> GetByUserId(Guid userId, CancellationToken cancellation);
    }
}