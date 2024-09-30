namespace PetFamily.Domain;

public record SpeciesBreed
{
    public SpeciesId SpeciesId { get; }
    public Guid BreedId { get; }
    
    public SpeciesBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
}