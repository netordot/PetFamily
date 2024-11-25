using Microsoft.Extensions.DependencyInjection;
using PetFamily.Species.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Species.Presentation
{
    public static class Inject
    {
        public static IServiceCollection AddSpeciesPresentation(this IServiceCollection services)
        {
            services.AddScoped<ISpeciesContract, SpeciesContract>();

            return services;
        }
    }
}
