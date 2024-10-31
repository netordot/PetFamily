using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
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

    public class FilesCleanerService : IFilesCleanerService
    {
        private readonly IFileProvider _fileProvider;
        private readonly IMessageQueue<IEnumerable<Application.Providers.FileProvider.FileInfo>> _messageQueue;

        public FilesCleanerService(IFileProvider fileProvider,
            IMessageQueue<IEnumerable<Application.Providers.FileProvider.FileInfo>> messageQueue)
        {
            _fileProvider = fileProvider;
            _messageQueue = messageQueue;

        }
        public async Task Process(CancellationToken stoppingToken)
        {
            var fileInfos = await _messageQueue.ReadAsync(stoppingToken);

            foreach (var fileInfo in fileInfos)
            {
                await _fileProvider.RemoveFile(fileInfo, stoppingToken);
            }
        }
    }
}
