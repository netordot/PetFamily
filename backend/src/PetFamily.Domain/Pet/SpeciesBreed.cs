using PetFamily.Domain.Pet.Species;
using PetFamily.SharedKernel.Id;

namespace PetFamily.Domain.Pet;

public record SpeciesBreed
{
    public SpeciesId SpeciesId { get; }
    public Guid BreedId { get; }

    private SpeciesBreed()
    {
        
    }
    
    public SpeciesBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
}