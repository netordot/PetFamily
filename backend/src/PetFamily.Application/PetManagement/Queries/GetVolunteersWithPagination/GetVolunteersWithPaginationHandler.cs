using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination
{
    public class GetVolunteersWithPaginationHandler : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;
        public GetVolunteersWithPaginationHandler(IReadDbContext context)
        {
            _readDbContext = context;
        }

        public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query, CancellationToken cancellation)
        {
            // TODO: добавить валидацию 
            var volunteersQuery = _readDbContext.Volunteers.AsQueryable();

            Expression<Func<VolunteerDto, object>> keySelector = query.SortBy.ToLower() switch
            {
                "experience" => (volunteer) => volunteer.Experience,
                
                // TODO: сделать по остальным так же
                _ => (volunteer) => volunteer.Id

            };

            volunteersQuery = query.SortDirection?.ToLower() == "desc"
                ? volunteersQuery.OrderByDescending(keySelector)
                : volunteersQuery.OrderBy(keySelector);


            // пример сортировки при пагинации 
            //volunteersQuery = volunteersQuery
            //    .WhereIf(!string.IsNullOrWhiteSpace(query.Title),
            //    v => v.Name.Contains(query.Title!));

            var result = await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellation);

            return result;
        }
    }

}
