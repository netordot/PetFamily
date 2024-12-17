using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Infrastructure.Data;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Managers
{
    public class VolunteerAccountManager : IVolunteerAccountManager
    {
        private readonly AccountsWriteDbContext _context;

        public VolunteerAccountManager(AccountsWriteDbContext context)
        {
            _context = context;
        }

        public async Task Add(VolunteerAccount volunteerAccount, CancellationToken cancellation)
        {
            await _context.AddAsync(volunteerAccount, cancellation);
        }

        public async Task<Result<VolunteerAccount, Error>> GetByUserId(Guid userId, CancellationToken cancellation)
        {
            var result = await _context.VolunteerAccounts.FirstOrDefaultAsync(v => v.UserId == userId);
            if (result == null)
            {
                return Errors.General.NotFound(userId);
            }

            return result;
        }

        public async Task<Result<VolunteerAccount, Error>> GetById(Guid id, CancellationToken cancellation)
        {
            var result = await _context.VolunteerAccounts.FirstOrDefaultAsync(v => v.Id == id);
            if (result == null)
            {
                return Errors.General.NotFound(id);
            }

            return result;
        }
    }
}
