using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Infrastructure.Data;
using PetFamily.VolunteerRequest.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddVolunteerRequestInfrastructure(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services
                .AddDatabase()
                .AddRepositories()
                .AddDbContexts();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModuleNames.VolunteerRequest);
            //services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVolunteerRequestRepository, VolunteerRequestRepository>();

            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services)
        {
            services.AddScoped<VolunteersRequestWriteDbContext>();
            services.AddScoped<IVolunteersRequestReadDbContext, VolunteersRequestReadDbContext>();

            return services;
        }

    }
}
