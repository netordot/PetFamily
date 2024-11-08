using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Species.GetBreedsWithPagination
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
