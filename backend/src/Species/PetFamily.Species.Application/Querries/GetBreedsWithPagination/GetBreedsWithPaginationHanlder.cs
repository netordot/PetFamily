using CSharpFunctionalExtensions;
using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Core.Models;
using PetFamily.Species.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Species.Application.Querries.GetBreedsWithPagination
{
    // TODO: добавить сортировку по критерию OrderBy, как сделал это в getAllVolunteersService
    public class GetBreedsWithPaginationHanlder : IQueryHandler<PagedList<BreedDto>, GetBreedsWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetBreedsWithPaginationHanlder(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<PagedList<BreedDto>> Handle(GetBreedsWithPaginationQuery query, CancellationToken cancellation)
        {
            var breedQuery = _readDbContext.Breeds.AsQueryable();

            breedQuery = breedQuery.Where(b => b.SpeciesId == query.id);

            var breeds = query.OrderBy?.ToLower() == "desc"
                ? breedQuery.OrderByDescending(k => k.Name)
                : breedQuery.OrderBy(k => k.Name);

            var result = await breeds.ToPagedList(query.Page, query.PageSize, cancellation);

            return result;
        }
    }
}
