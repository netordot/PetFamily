using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain.AggregateRoot;
using PetFamily.Volunteers.Infrastructure.Data;

namespace PetFamily.Volunteers.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly WriteDbContext _context;

    public VolunteerRepository(WriteDbContext context)
    {
        _context = context;
    }

   // пока что тут лежит saveChanges, есть проблема с unit of work, c di связана, скоро будет задача на это
    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _context.Volunteers.AddAsync(volunteer);
        await  _context.SaveChangesAsync();

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken = default)
    {
        var volunteer = await _context.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer == null)
        {
            return Errors.General.NotFound(volunteerId);
        }

        return volunteer;
    }
    
    public async Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _context.Volunteers.Attach(volunteer);
        await _context.SaveChangesAsync(cancellationToken);
        return volunteer.Id.Value;
    }

    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _context.Volunteers.Remove(volunteer);
        _context.SaveChanges();
        return volunteer.Id.Value;
    }

}