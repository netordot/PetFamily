using PetFamily.Application.Messaging;
using PetFamily.Application.Providers;

namespace PetFamily.Infrastructure.BackGroundServices
{
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
