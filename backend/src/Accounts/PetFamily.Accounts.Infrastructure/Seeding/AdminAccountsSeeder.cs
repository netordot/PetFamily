using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Seeding
{
    public class AdminAccountsSeeder
    {
        private readonly IServiceScopeFactory _serviceScope;

        public AdminAccountsSeeder(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScope = serviceScopeFactory;
        }

        public async Task Seed(CancellationToken cancellation)
        {
            var service = _serviceScope.CreateScope();
            var scope = service.ServiceProvider.GetRequiredService<AdminAccountsSeederService>();

            await scope.Seed(cancellation);
        }
    }
}
