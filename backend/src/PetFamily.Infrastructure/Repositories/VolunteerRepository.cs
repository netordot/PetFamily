using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
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
        //    return Error.Failure("get.volunteer","An error occurred while retrieving volunteer");
        //}

        var volunteer = await _context.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        int a = 10;

        if (volunteer == null)
        {
            return Errors.General.NotFound(volunteerId);
        }

        return volunteer;
    }

    //public async Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    //{
    //    _context.Volunteers.Attach(volunteer);
    //    await _context.SaveChangesAsync(cancellationToken);

    //    return volunteer.Id.Value;
    //}

    public async Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Volunteers.Attach(volunteer);

            foreach (var pet in volunteer.Pets)
            {
                // Set the correct state for pets
                _context.Entry(pet).State = pet.Id.Value == Guid.Empty ? EntityState.Added : EntityState.Modified;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "An error occurred while saving volunteer with ID: {VolunteerId}", volunteer.Id);
            return Error.Failure("update.volunteer","An error occurred while saving volunteer");
        }
    }

    //public async Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var existingVolunteer = volunteer;

    //            _context.Entry(existingVolunteer).CurrentValues.SetValues(volunteer);

    //            // Handle the Pets relationship
    //            foreach (var pet in volunteer.Pets)
    //            {
    //                var existingPet = existingVolunteer.Pets
    //                    .FirstOrDefault(p => p.Id == pet.Id);

    //                if (existingPet != null)
    //                {
    //                    _context.Entry(existingPet).CurrentValues.SetValues(pet);
    //                }
    //                else
    //                {
    //                    existingVolunteer.Pets.Add(pet);
    //                }
    //            }

    //            // Remove deleted pets
    //            foreach (var existingPet in existingVolunteer.Pets.ToList())
    //            {
    //                if (!volunteer.Pets.Any(p => p.Id == existingPet.Id))
    //                {
    //                    existingVolunteer.Pets.Remove(existingPet);
    //                }
    //            }


    //        await _context.SaveChangesAsync(cancellationToken);
    //        return volunteer.Id.Value;
    //    }
    //    catch (Exception ex)
    //    {
    //        return Error.Validation("update.issue","An error occurred while saving volunteer");
    //    }
    //}


    public async Task<Result<Guid, Error>> Delete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _context.Volunteers.Remove(volunteer);

        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

}