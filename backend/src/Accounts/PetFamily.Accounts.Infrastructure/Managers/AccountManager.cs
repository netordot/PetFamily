using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Accounts.Infrastructure.Data;
using PetFamily.Application.AccountManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly AccountsDbContext _accountsDbContext;

        public AccountManager(AccountsDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext;
        }

        public async Task AddAdminAccount(AdminAccount adminAccount)
        {
            var account = await _accountsDbContext.AdminAccounts
                .FirstOrDefaultAsync(a => a.FullName == adminAccount.FullName);

            if (account != null)
            {
                return;
            }

            await _accountsDbContext.AdminAccounts.AddAsync(adminAccount);
            await _accountsDbContext.SaveChangesAsync();
        }

        public async Task AddParticipantAccount(ParticipantAccount participantAccount)
        {
            await _accountsDbContext.ParticipantAccounts.AddAsync(participantAccount);
            await _accountsDbContext.SaveChangesAsync();
        }
    }
}
