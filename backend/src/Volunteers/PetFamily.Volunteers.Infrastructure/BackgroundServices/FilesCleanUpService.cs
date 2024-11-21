
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetFamily.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.BackGroundServices
{
    public class FilesCleanUpService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        // консьюмер

        public FilesCleanUpService(
            IFileProvider fileProvider,
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await using var scope = _serviceProvider.CreateAsyncScope();

                var scopeFileService = _serviceProvider.GetRequiredService<IFilesCleanerService>();
                while(!stoppingToken.IsCancellationRequested)
                {
                    await scopeFileService.Process(stoppingToken);
                }
              
                await Task.Delay(5000, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
