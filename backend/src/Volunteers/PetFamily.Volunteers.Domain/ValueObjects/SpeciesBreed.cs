using PetFamily.SharedKernel.Id;

namespace PetFamily.Volunteers.Domain.ValueObjects;

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