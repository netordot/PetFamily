
namespace PetFamily.Volunteers.Presentation
{
    public interface IVolunteersContract
    {
        Task<bool> IsBreedInUse(Guid breedId, CancellationToken cancellation);
        Task<bool> IsSpeciesInUse(Guid speciesId, CancellationToken cancellation);
    }
}