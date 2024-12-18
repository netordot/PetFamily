using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.VolunteerRequest;
using PetFamily.Core.Models;
using PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticipant
{
    public class GetVolunteerRequestsForParticipantHandler : IQueryHandler<PagedList<VolunteerRequestDto>, GetVolunteerRequestsForParticipantQuery>
    {
        private readonly IVolunteersRequestReadDbContext _readContext;

        public GetVolunteerRequestsForParticipantHandler(IVolunteersRequestReadDbContext context)
        {
            _readContext =  context;
        }
        public async Task<PagedList<VolunteerRequestDto>> Handle(GetVolunteerRequestsForParticipantQuery query, CancellationToken cancellation)
        {
            var volunteerQuery = ApplyFiltration(_readContext.VolunteerRequests.Where(v => v.UserId == query.ParticipantId), query);

            var result = await volunteerQuery.ToPagedList<VolunteerRequestDto>(query.Page, query.PageSize, cancellation);

            return result;  
        }

        private static IQueryable<VolunteerRequestDto> ApplyFiltration(IQueryable<VolunteerRequestDto> volunteerRequestsQuery, GetVolunteerRequestsForParticipantQuery query)
        {
            return volunteerRequestsQuery
                .WhereIf(query.VolunteerRequestStatus != null, v => v.Status == query.VolunteerRequestStatus)
                .WhereIf(!string.IsNullOrEmpty(query.FirstName), v => v.FirstName.Contains(query.FirstName!))
                .WhereIf(!string.IsNullOrEmpty(query.SecondName), v => v.SecondName.Contains(query.SecondName!))
                .WhereIf(!string.IsNullOrEmpty(query.LastName), v => v.LastName.Contains(query.LastName!))
                .WhereIf(!string.IsNullOrEmpty(query.Email), v => v.Email.Contains(query.Email!))
                .WhereIf(!string.IsNullOrEmpty(query.PhoneNumber), v => v.PhoneNumber.Contains(query.PhoneNumber!))
                .WhereIf(!string.IsNullOrEmpty(query.Description), v => v.Description.Contains(query.Description!))
                .WhereIf(!string.IsNullOrEmpty(query.RejectionComment), v => v.RejectionComment.Contains(query.RejectionComment!))
                .WhereIf(query.Experience != null, v => v.Experience == query.Experience);
                
        }
    }
}
