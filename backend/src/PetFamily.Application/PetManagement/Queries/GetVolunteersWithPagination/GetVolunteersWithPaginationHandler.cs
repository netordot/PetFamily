using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var volunteersQuery = _readDbContext.Volunteers.AsQueryable();

            var result = await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellation);   

            return result;
        }
    }

}
