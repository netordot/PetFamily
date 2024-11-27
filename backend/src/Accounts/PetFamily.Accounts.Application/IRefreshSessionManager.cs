using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application
{
    public interface IRefreshSessionManager
    {
        void DeleteSession(RefreshSession session, CancellationToken cancellation);
        Task<Result<RefreshSession, ErrorList>> GetSessionByToken(Guid RefreshToken, CancellationToken cancellationToken);
        Task<Result<RefreshSession, ErrorList>> GetSessionByUserId(Guid userId, CancellationToken cancellationToken);
    }
}