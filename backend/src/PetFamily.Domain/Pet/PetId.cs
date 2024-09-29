namespace PetFamily.Domain;

public class PetId
{
    public Guid Value { get; private set; }
    private PetId(Guid value)
    {
        Value = value;
    }
    
    public static PetId NewPetId => new(Guid.NewGuid());
    public static PetId Empty => new(Guid.Empty); 
    public static PetId Create(Guid id) => new(id);

}