namespace PetFamily.Domain.Shared;

public class Requisites
{
    public IReadOnlyList<Requisite> Value { get; private set; }

    private Requisites()
    {
        
    }

    public Requisites(List<Requisite> value)
    {
        Value = value;
    }

    public static Requisites Create(List<Requisite> value)
    {
        return new Requisites(value);
    }
}