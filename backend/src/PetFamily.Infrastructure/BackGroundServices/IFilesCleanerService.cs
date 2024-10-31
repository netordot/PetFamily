namespace PetFamily.Infrastructure.BackGroundServices
{
    public interface IFilesCleanerService
    {
        Task Process(CancellationToken stoppingToken);
    }
}
