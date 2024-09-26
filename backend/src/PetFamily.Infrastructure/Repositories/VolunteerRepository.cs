using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteerRepository
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

    public async Task<Result<Volunteer>> GetById(Guid volunteerId, CancellationToken cancellationToken = default)
    {
        var volunteer = await _context.Volunteers
            .Include(p => p.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);
        if (volunteer == null)
        {
            return "Volunteer not found";
        }
            // добавить result pattern используя самописный result и валидацию
        return volunteer;
    }
    
    
}