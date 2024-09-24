namespace PetFamily.Domain;

public class BreedId
{
    public Guid Value { get; set; }

    private BreedId(Guid value)
    {
        Value = value;
    }
    
    public static BreedId NewBreedId (Guid value) => new(Guid.NewGuid());
    public static BreedId Empty => new(Guid.Empty);
    public static BreedId Create(Guid id) => new(id);
}