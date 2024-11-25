using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Providers
{
    public interface IUnitOfWork
    {
        Task<IDbTransaction> BeginTransaction(CancellationToken cancellation);
        Task SaveChanges(CancellationToken cancellation);
    }
}
