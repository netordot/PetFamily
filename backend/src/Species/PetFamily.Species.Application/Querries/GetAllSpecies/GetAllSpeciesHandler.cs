using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Core.Models;
using PetFamily.Species.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Species.Application.Querries.GetAllSpecies
{
    public class GetAllSpeciesHandler : IQueryHandler<PagedList<SpeciesDto>, GetAllSpeciesWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetAllSpeciesHandler(IReadDbContext context)
        {
            _readDbContext = context;
        }

        public async Task<PagedList<SpeciesDto>> Handle(GetAllSpeciesWithPaginationQuery query, CancellationToken cancellation)
        {

            var speciesQuery = _readDbContext.Species.AsQueryable();

            speciesQuery.Include(s => s.Breeds);

            speciesQuery = query.SortOrder?.ToLower() == "desc"
                ? speciesQuery.OrderByDescending(keySelector => keySelector.Name)
                : speciesQuery.OrderBy(keySelector => keySelector.Name);

            var result = await speciesQuery.ToPagedList(query.Page, query.PageSize, cancellation);

            return result;
        }
    }
}
