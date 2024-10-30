using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _context;

    public VolunteerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _context.Volunteers.AddAsync(volunteer);

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken = default)
    {
        var volunteer = await _context.Volunteers
            .Include(v => v.Pets)
            .ThenInclude(p => p.Photos)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer == null)
        {
            return Errors.General.NotFound(volunteerId);
        }

        return volunteer;
    }

    public Guid Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _context.Volunteers.Attach(volunteer);

        return volunteer.Id.Value;
    }

    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _context.Volunteers.Remove(volunteer);

        return volunteer.Id.Value;
    }

}