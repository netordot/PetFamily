namespace PetFamily.Core.Providers
{
    public interface IFilesCleanerService
    {
        Task Process(CancellationToken stoppingToken);
    }
}
