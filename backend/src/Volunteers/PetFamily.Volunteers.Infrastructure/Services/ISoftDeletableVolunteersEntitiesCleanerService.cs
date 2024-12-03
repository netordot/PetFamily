
namespace PetFamily.Volunteers.Infrastructure.Services
{
    public interface ISoftDeletableVolunteersEntitiesCleanerService
    {
        //Task DeletePets(CancellationToken cancellation);
        Task DeleteVolunteers(CancellationToken cancellation);
        Task Process(CancellationToken cancellation);
    }
}