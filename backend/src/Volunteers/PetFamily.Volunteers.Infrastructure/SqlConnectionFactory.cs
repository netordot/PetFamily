﻿using Microsoft.Extensions.Configuration;
using Npgsql;
using PetFamily.Volunteers.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure
{
    public class SqlConnectionFactory : ISqlConnectionFactory
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
