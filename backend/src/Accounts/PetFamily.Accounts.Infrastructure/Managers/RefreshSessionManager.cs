using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Accounts.Infrastructure.Data;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Managers
{
    public class RefreshSessionManager : IRefreshSessionManager
    {
        private readonly AccountsDbContext _context;

        public RefreshSessionManager(AccountsDbContext context)
        {
            _context = context;
        }

        public async Task<Result<RefreshSession, ErrorList>> GetSessionByToken(
            Guid RefreshToken,
            CancellationToken cancellationToken)
        {
            var session = await _context.RefreshSessions
                .FirstOrDefaultAsync(s => s.RefreshToken == RefreshToken);
            if (session == null)
            {
                return Errors.General.NotFound(RefreshToken).ToErrorList();
            }

            return session;
        }

        public async Task<Result<RefreshSession, ErrorList>> GetSessionByUserId(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var session = await _context.RefreshSessions
                .FirstOrDefaultAsync(s => s.UserId == userId);
            if (session == null)
            {
                return Errors.General.NotFound(userId).ToErrorList();
            }

            return session;
        }

        public void DeleteSession(RefreshSession session, CancellationToken cancellation)
        {
            _context.RefreshSessions.Remove(session);
        }
    }
}
