namespace PetFamily.Domain;

public record SpeciesBreed
{
    public SpeciesId SpeciesId { get; private set; }
    public Guid BreedId { get; private set ; }

    private SpeciesBreed()
    {
        
    }

    public SpeciesBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
}