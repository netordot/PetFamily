using PetFamily.Application.Providers;
using PetFamily.Core.Messaging;
using PetFamily.Core.Providers;

namespace PetFamily.Infrastructure.BackGroundServices
{
    public class FilesCleanerService : IFilesCleanerService
    {
        private readonly IFileProvider _fileProvider;
        private readonly IMessageQueue<IEnumerable<Core.Providers.FileInfo>> _messageQueue;

        public FilesCleanerService(IFileProvider fileProvider,
            IMessageQueue<IEnumerable<Core.Providers.FileInfo>> messageQueue)
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
