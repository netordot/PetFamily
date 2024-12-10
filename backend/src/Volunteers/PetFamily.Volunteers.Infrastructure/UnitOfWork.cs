using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Core.Providers;
using PetFamily.Volunteers.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PetFamily.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WriteDbContext _context;

        public UnitOfWork(WriteDbContext context)
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
