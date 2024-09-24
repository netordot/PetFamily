namespace PetFamily.Domain;

public class SpeciesId
{
    public Guid Value { get; set; }

    private SpeciesId(Guid value)
    {
        Value = value;
    }
    
    public static SpeciesId NewPetPhotoId (Guid value) => new(Guid.NewGuid());
    public static SpeciesId Empty => new(Guid.Empty);
    public static SpeciesId Create(Guid id) => new(id);
}