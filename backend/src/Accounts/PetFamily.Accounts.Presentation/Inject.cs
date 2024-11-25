using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Presentation
{
    public static class Inject
    {
        public static IServiceCollection AddAccountsPresentation(this IServiceCollection services)
        {
            services.AddScoped<IAccountsContract, AccountsContract>();

            return services;
        }
    }
}
