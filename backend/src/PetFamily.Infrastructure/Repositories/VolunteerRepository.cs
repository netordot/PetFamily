using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain;
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
        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken = default)
    {

        //try
        //{
        //    var volunteer = await _context.Volunteers
        //        .AsNoTracking() // Add this line to ensure no tracking issues
        //        .Where(v => v.Id == volunteerId)
        //        .FirstOrDefaultAsync(cancellationToken);

        //    if (volunteer == null)
        //    {
        //        return Errors.General.NotFound(volunteerId);
        //    }

        //    // Optionally, load pets separately
        //    _context.Entry(volunteer)
        //        .Collection(v => v.Pets)
        //        .Load();

        //    return volunteer;
        //}
        //catch (Exception ex)
        //{
        //    return Error.Failure("get.volunteer", "An error occurred while retrieving volunteer");
        //}

        var volunteer = await _context.Volunteers
            .Include(v => v.Pets)
            .ThenInclude(p => p.Photos)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        int a = 10;

        if (volunteer == null)
        {
            return Errors.General.NotFound(volunteerId);
        }

        return volunteer;
    }

    public async Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _context.Volunteers.Attach(volunteer);
        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Result<Guid, Error>> Delete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _context.Volunteers.Remove(volunteer);

        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Result<Volunteer, Error>> GetVolunteerByPetId(PetId petId)
    {
        var volunteer = await _context
            .Volunteers
            .Include(p => p.Pets)
            .ThenInclude(ph => ph.Photos)
            .FirstOrDefaultAsync(v => v.Pets.Any(p => p.Id == petId));
        
        if(volunteer ==null)
            return Errors.General.NotFound();

        return volunteer;
    }

}