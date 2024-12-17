using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Contracts
{
    public interface IAccountsContract
    {
        Task<bool> CheckUserPermission(Guid userId, string permissionCode);

        Task<Result<Guid, ErrorList>> CreateVolunteerAccount(
           Guid userId,
           int experience,
           List<Requisite> requisites,
           CancellationToken cancellation);
    }
}