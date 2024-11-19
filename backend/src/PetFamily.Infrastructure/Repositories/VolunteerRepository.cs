using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.PetManagement.Commands.Volunteers;
using PetFamily.Domain;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Volunteer;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly WriteDbContext _context;

    public VolunteerRepository(WriteDbContext context)
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