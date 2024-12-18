using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Core.Dtos.VolunteerRequest;
using PetFamily.Core.Models;
using PetFamily.SharedKernel.Id;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForAdmin
{
    public class GetVolunteerRequestsForAdminHandler : IQueryHandler<PagedList<VolunteerRequestDto>, GetVolunteerRequestsForAdminQuery>
    {
        private readonly IVolunteersRequestReadDbContext _readContext;

        public GetVolunteerRequestsForAdminHandler(IVolunteersRequestReadDbContext readDbContext)
        {
            _readContext = readDbContext;
        }
        public async Task<PagedList<VolunteerRequestDto>> Handle(GetVolunteerRequestsForAdminQuery query, CancellationToken cancellation)
        {
            var requestsQuery = ApplyFiltration(_readContext.VolunteerRequests
                             .Where(v => v.AdminId == query.AdminId), query);
                        
            var result = await requestsQuery.ToPagedList(query.Page, query.PageSize, cancellation);

            return result;
        }

        private static IQueryable<VolunteerRequestDto> ApplyFiltration(IQueryable<VolunteerRequestDto> volunteerRequestsQuery, GetVolunteerRequestsForAdminQuery query)
        {
            return
                volunteerRequestsQuery.WhereIf(query.StatusDto != 0, v => v.Status == query.StatusDto);  
        }

    }
}
