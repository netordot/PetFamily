using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Providers;
using PetFamily.Discussion.Application;
using PetFamily.Discussion.Infrastructure.Data;
using PetFamily.Discussion.Infrastructure.Repositories;
using PetFamily.SharedKernel.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddDiscussionInfrastructure(this IServiceCollection services,
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
            services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModuleNames.Discussion);
            //services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDiscussionsRepository, DiscussionsRepository>();

            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services)
        {
            services.AddScoped<DiscussionWriteDbContext>();
            services.AddScoped<IDiscussionReadDbContext, DiscussionReadDbContext>();

            return services;
        }
    }
}