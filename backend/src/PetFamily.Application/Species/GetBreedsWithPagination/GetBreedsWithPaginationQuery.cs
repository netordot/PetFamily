using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Species.GetBreedsWithPagination
{
    public record GetBreedsWithPaginationQuery(Guid id, int Page, int PageSize, string? SortBy, string? OrderBy ): IQuery;
    
}
