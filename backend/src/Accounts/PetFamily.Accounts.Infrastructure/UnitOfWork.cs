using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Accounts.Infrastructure.Data;
using PetFamily.Core.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountsWriteDbContext _context;

        public UnitOfWork(AccountsWriteDbContext context)
        {
            _context = context;
        }

        public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellation = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellation);

            return transaction.GetDbTransaction();
        }

        public async Task SaveChanges(CancellationToken cancellation = default)
        {
            await _context.SaveChangesAsync(cancellation);
        }

    }
}
