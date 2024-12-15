using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Infrastructure.Repositories
{
    public class VolunteerRequestRepository : IVolunteerRequestRepository
    {
        private readonly VolunteersRequestWriteDbContext _context;

        public VolunteerRequestRepository(VolunteersRequestWriteDbContext context)
        {
            _context = context;
        }

        public async Task Add(
            VolunteerRequest.Domain.AggregateRoot.VolunteerRequest volunteerRequest,
            CancellationToken cancellationToken)
        {
            await _context.VolunteerRequests.AddAsync(volunteerRequest, cancellationToken);
        }

        public async Task<Result<VolunteerRequest.Domain.AggregateRoot.VolunteerRequest, Error>> GetById(Guid requestId, CancellationToken cancellation)
        {
            var volunteerRequest = await _context.VolunteerRequests
                .FirstOrDefaultAsync(v => v.Id == requestId, cancellation);

            if (volunteerRequest == null)
            {
                return Errors.General.NotFound(requestId);
            }

            return volunteerRequest;
        }

        public async Task<Result<VolunteerRequest.Domain.AggregateRoot.VolunteerRequest, Error>> GetByUserId(Guid userId, CancellationToken cancellation)
        {
            var volunteerRequest = await _context.VolunteerRequests
                .FirstOrDefaultAsync(v => v.UserId == userId, cancellation);

            if (volunteerRequest == null)
            {
                return Errors.General.NotFound(userId);
            }

            return volunteerRequest;
        }
    }
}
