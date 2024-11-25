using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Presentation
{
    public static class Inject
    {
        public static IServiceCollection AddVolunteersPresentation(this IServiceCollection services)
        {
            services.AddScoped<IVolunteersContract, VolunteersContract>();

            return services; 
        }
    }
}
