namespace PetFamily.SharedKernel.Id;

public record PetId
{
    public Guid Value { get; }
    private PetId(Guid value)
    {
        Value = value;
    }

    private PetId()
    {

    }

    public static PetId NewPetId => new(Guid.NewGuid());
    public static PetId Empty => new(Guid.Empty);
    public static PetId Create(Guid id) => new(id);

}