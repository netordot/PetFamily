using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync
            (DbContextEventData eventData, InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if(eventData.Context is null)
            {
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = eventData.Context.ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

            foreach(var entry in entries)
            {
                entry.State = EntityState.Modified;
                entry.Entity.Delete();
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        }
    }
}
