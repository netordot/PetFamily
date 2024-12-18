using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Domain.AggregateRoot;
using PetFamily.VolunteerRequest.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Infrastructure.Repositories
{
    public class UserRestrictionsRepository : IUserRestrictionsRepository
    {
        private readonly VolunteersRequestWriteDbContext _context;

        public UserRestrictionsRepository(VolunteersRequestWriteDbContext context)
        {
            _context = context;
        }

        public async Task<Result<UserRestriction, Error>> GetByDiscussionId(Guid id, CancellationToken cancellation)
        {
            var userRestriction = await _context.UserRestrictions
                .FirstOrDefaultAsync(u => u.DiscussionId == id);

            if (userRestriction == null)
            {
                return Errors.General.NotFound(id);
            }

            return userRestriction;
        }

        public async Task<Result<Guid, Error>> Add(UserRestriction userRestriction, CancellationToken cancellation)
        {
            await _context.UserRestrictions.AddAsync(userRestriction, cancellation);

            return userRestriction.Id.Value;
        }
    }
}
