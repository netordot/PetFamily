using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Discussion.Application;
using PetFamily.Discussion.Domain.AggregateRoot;
using PetFamily.Discussion.Infrastructure.Data;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Infrastructure.Repositories
{
    public class DiscussionsRepository : IDiscussionsRepository
    {
        private readonly DiscussionWriteDbContext _context;

        public DiscussionsRepository(DiscussionWriteDbContext context)
        {
            _context = context;
        }

        public async Task Add(Discussion.Domain.AggregateRoot.Discussion discussion, CancellationToken cancellation)
        {
            await _context.AddAsync(discussion, cancellation);
        }

        public async Task<Result<Discussion.Domain.AggregateRoot.Discussion, Error>> GetById(Guid Id, CancellationToken cancellation)
        {
            var discussion = await _context.Discussions
                .Include(d => d.Messages)
                .FirstOrDefaultAsync(ds => ds.Id == Id, cancellation);

            if (discussion == null)
            {
                return Errors.General.NotFound(Id);
            }

            return discussion;
        }
    }
}
