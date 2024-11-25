using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Querries.GetVolunteersWithPagination
{
    public record GetVolunteersWithPaginationQuery(int Page, int PageSize, string? SortBy, string? SortDirection) : IQuery;
}
