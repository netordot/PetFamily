using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.VolunteerRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForAdmin
{
    public record GetVolunteerRequestsForAdminQuery(int Page, int PageSize, Guid AdminId, VolunteerRequestStatusDto StatusDto) : IQuery;
    
}
