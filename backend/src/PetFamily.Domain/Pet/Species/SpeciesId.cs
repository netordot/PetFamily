namespace PetFamily.Domain.Pet.Species;

public record SpeciesId
{
    public Guid Value { get; }

    private SpeciesId(Guid value)
    {
        Value = value;
    }

    private SpeciesId()
    {
        
    }

    public static SpeciesId NewSpeciesId => new(Guid.NewGuid());
    public static SpeciesId Empty => new(Guid.Empty);
    public static SpeciesId Create(Guid id) => new(id);

    public static implicit operator Guid(SpeciesId speciesId)
    {
        ArgumentNullException.ThrowIfNull(speciesId);
        return speciesId.Value;
    }
}