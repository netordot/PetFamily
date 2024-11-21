namespace PetFamily.Species.Contracts
{
    public interface ISpeciesContract
    {
        Task<bool> SpeciesBreedExists(Guid speciesId, Guid breedId, CancellationToken cancellation);
    }
}