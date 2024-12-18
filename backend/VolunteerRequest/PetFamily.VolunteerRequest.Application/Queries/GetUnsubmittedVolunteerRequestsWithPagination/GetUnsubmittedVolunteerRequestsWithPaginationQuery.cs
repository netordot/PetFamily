using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Queries.GetUnsubmittedVolunteerRequestsWithPagination
{
    public record GetUnsubmittedVolunteerRequestsWithPaginationQuery(
    int Page,
    int PageSize) : IQuery;
}
