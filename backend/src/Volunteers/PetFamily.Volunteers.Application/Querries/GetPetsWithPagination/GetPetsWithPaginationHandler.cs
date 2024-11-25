using PetFamily.Application.Extensions;
using PetFamily.Application.PetManagement.Queries.GetPetsWithPagination.GetPetsWithPagination;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Core.Models;
using PetFamily.Volunteers.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PetFamily.Volunteers.Application.Querries.GetPetsWithPagination
{
    public class GetPetsWithPaginationHandler : IQueryHandler<PagedList<PetDto>, GetPetsWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetPetsWithPaginationHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<PagedList<PetDto>> Handle(GetPetsWithPaginationQuery query, CancellationToken cancellation)
        {
            var petsQuery = ApplyFiltration(_readDbContext.Pets, query);

            var keySelector = SortByProperty(query.SortBy);

            petsQuery = query.SortDirection?.ToLower() == "desc"
                ? petsQuery.OrderByDescending(keySelector)
                : petsQuery.OrderBy(keySelector);

            var result = await petsQuery.ToPagedList(query.Page, query.PageSize, cancellation);

            return result;
        }

        private static IQueryable<PetDto> ApplyFiltration(IQueryable<PetDto> petsQuery, GetPetsWithPaginationQuery query)
        {
            return petsQuery
                .WhereIf(!string.IsNullOrEmpty(query.NickName), pet => pet.Name.Contains(query.NickName!))
                .WhereIf(!string.IsNullOrEmpty(query.City), pet => pet.City.Contains(query.City!))
                .WhereIf(!string.IsNullOrEmpty(query.Street), pet => pet.City.Contains(query.Street!))
                .WhereIf(query.MinAge.HasValue, pet => (DateTime.Now - pet.DateOfBirth).TotalDays / 365 >= query.MinAge)
                .WhereIf(query.MaxAge.HasValue, pet => (DateTime.Now - pet.DateOfBirth).TotalDays / 365 <= query.MaxAge)
                .WhereIf(query.SpeciesId.HasValue, pet => pet.SpeciesId == query.SpeciesId)
                .WhereIf(query.BreedId.HasValue, pet => pet.BreedId == query.BreedId)
                .WhereIf(query.MinHeight.HasValue, pet => pet.Height >= query.MinHeight)
                .WhereIf(query.MaxHeight.HasValue, pet => pet.Height <= query.MaxHeight)
                .WhereIf(query.MinWeight.HasValue, pet => pet.Height >= query.MinWeight)
                .WhereIf(query.MaxWeight.HasValue, pet => pet.Height <= query.MaxWeight)
                .WhereIf(query.VolunteerId.HasValue, pet => pet.VolunteerId == query.VolunteerId)
                .WhereIf(query.VolunteerId.HasValue, pet => pet.VolunteerId == query.VolunteerId)
                .WhereIf(query.IsCastrated.HasValue, pet => pet.IsCastrated == query.IsCastrated)
                .WhereIf(query.IsVaccinated.HasValue, pet => pet.IsVaccinated == query.IsVaccinated)
                .WhereIf(query.HelpStatus.HasValue, pet => pet.Status == query.HelpStatus);
        }

        private Expression<Func<PetDto, object>> SortByProperty(string? sortBy)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                return volunteer => volunteer.Id;
            }

            Expression<Func<PetDto, object>> keySelector = sortBy.ToLower() switch
            {
                "name" => prop => prop.Name,
                "species" => prop => prop.SpeciesId,
                "breed" => prop => prop.BreedId,
                "street" => prop => prop.Street,
                "city" => prop => prop.City,
                "buildingnumber" => prop => prop.BuildingNumber,
                "volunteer" => prop => prop.VolunteerId,
                "height" => prop => prop.Height,
                "weight" => prop => prop.Weight,

                _ => (prop) => prop.VolunteerId

            };

            return keySelector;
        }
    }
}
