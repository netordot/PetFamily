using PetFamily.Core.Dtos.VolunteerRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Presentation.VolunteerRequest.Contracts
{
    public record GetWithPaginationForAdminRequest(int Page, int PageSize, VolunteerRequestStatusDto Status);
   
}
