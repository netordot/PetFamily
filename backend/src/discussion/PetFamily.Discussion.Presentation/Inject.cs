using Microsoft.Extensions.DependencyInjection;
using PetFamily.Discussion.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Presentation
{
    public static class Inject
    {
        public static IServiceCollection AddDiscussionPresentation(this IServiceCollection services)
        {
            services.AddScoped<IDiscussionContract,  DiscussionContract>();

            return services;
        }

    }
}
