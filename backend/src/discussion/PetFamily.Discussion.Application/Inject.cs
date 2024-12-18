using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application
{
    public static class Inject
    {
        public static IServiceCollection AddDiscussionApplication(this IServiceCollection services)
        {

            services.AddCommands()
                .AddQueries()
                //.AddValidatorsFromAssembly(typeof(Inject).Assembly)
                ;

            return services;
        }

        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
                .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
                .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
        }
    }
}
