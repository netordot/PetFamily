using Dapper;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination
{
    public class GetVolunteersWithPaginationHandlerDapper : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetVolunteersWithPaginationHandlerDapper(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }


        public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query, CancellationToken cancellation)
        {
            var connection = _sqlConnectionFactory.Create();
            connection.Open();

            var parameters = new DynamicParameters();

            var volunteers = await connection.QueryAsync<VolunteerDto>
                (
                """

                """,
                parameters
                );



        }
    }
}
