using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetFamily.Application.Database.ISqlConnectionFactory;

namespace PetFamily.Infrastructure
{
    public class SqlConnectionFactory : Application.Database.ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create() =>
            new NpgsqlConnection(_configuration.GetConnectionString("Database"));
    }
}
