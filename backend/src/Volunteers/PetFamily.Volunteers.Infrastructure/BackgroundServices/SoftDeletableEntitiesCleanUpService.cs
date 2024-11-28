using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetFamily.Volunteers.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace PetFamily.Volunteers.Infrastructure.BackgroundServices
{
    //public class SoftDeletableEntitiesCleanUpService : BackgroundService
    //{
    //    private readonly ISoftDeletableVolunteersEntitiesCleanerService _cleanerService;
    //    public const int SOFT_DELETE_FREQUENCY = 24;

    //    public SoftDeletableEntitiesCleanUpService(
    //        ISoftDeletableVolunteersEntitiesCleanerService softDeletableVolunteersEntitesCleanerService,
    //        IServiceScopeFactory services
    //        )
    //    {
    //        _cleanerService = softDeletableVolunteersEntitesCleanerService;
    //        Services = services;
    //    }
    //    public IServiceScopeFactory Services { get; }
    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        await DoWork(stoppingToken);
    //    }
    //    public async Task DoWork(CancellationToken stoppingToken)
    //    {
    //        while (stoppingToken.IsCancellationRequested == false)
    //        {
    //            await using var scope = Services.CreateAsyncScope();

    //            var softDeletableService = scope.ServiceProvider
    //                .GetRequiredService<ISoftDeletableVolunteersEntitiesCleanerService>();

    //            await softDeletableService.Process(stoppingToken);


    //            await Task.Delay(TimeSpan.FromHours(SOFT_DELETE_FREQUENCY));
    //        }

    //        await Task.CompletedTask;

    //    }

    //    public override Task StopAsync(CancellationToken cancellationToken)
    //    {
    //        return base.StopAsync(cancellationToken);
    //    }
    //}

    public class SoftDeletableEntitiesCleanUpService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public const int SOFT_DELETE_FREQUENCY = 24;

        public SoftDeletableEntitiesCleanUpService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWork(stoppingToken);
                await Task.Delay(TimeSpan.FromHours(SOFT_DELETE_FREQUENCY), stoppingToken);
            }
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var softDeletableService = scope.ServiceProvider
                    .GetRequiredService<ISoftDeletableVolunteersEntitiesCleanerService>();

                await softDeletableService.Process(stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }

}
