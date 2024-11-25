using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Species.Application.Querries.GetAllSpecies
{
    public record GetAllSpeciesWithPaginationQuery(int Page, int PageSize, string? SortOrder) : IQuery;

}
