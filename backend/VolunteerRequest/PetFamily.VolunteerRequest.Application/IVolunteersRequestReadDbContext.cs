using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Dtos.VolunteerRequest;

namespace PetFamily.VolunteerRequest.Application
{
    public interface IVolunteersRequestReadDbContext
    {
        DbSet<VolunteerRequestDto> VolunteerRequests { get; set; }
    }
}