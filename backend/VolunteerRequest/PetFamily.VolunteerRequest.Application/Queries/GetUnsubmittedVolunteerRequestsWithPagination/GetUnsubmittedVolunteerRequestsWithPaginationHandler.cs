using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Core.Dtos.VolunteerRequest;
using PetFamily.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Queries.GetUnsubmittedVolunteerRequestsWithPagination
{
    public class GetUnsubmittedVolunteerRequestsWithPaginationHandler : IQueryHandler<PagedList<VolunteerRequestDto>, GetUnsubmittedVolunteerRequestsWithPaginationQuery>
    {
        private readonly IVolunteersRequestReadDbContext _readContext;

        public GetUnsubmittedVolunteerRequestsWithPaginationHandler(IVolunteersRequestReadDbContext dbContext)
        {
            _readContext = dbContext;   
        }
        public async Task<PagedList<VolunteerRequestDto>> Handle(GetUnsubmittedVolunteerRequestsWithPaginationQuery query, CancellationToken cancellation)
        {
            var requestsQuery = _readContext.VolunteerRequests
                 .Where(v => v.Status == Core.Dtos.VolunteerRequest.VolunteerRequestStatusDto.Waiting);

           var result = await requestsQuery.ToPagedList(query.Page, query.PageSize, cancellation);

            return result;
        }
    }
}
