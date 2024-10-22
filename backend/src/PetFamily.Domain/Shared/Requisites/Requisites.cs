namespace PetFamily.Domain.Shared.Requisites;

public record Requisites
{
    public IReadOnlyList<Requisite> Value { get; }

    private Requisites()
    {
        
    }

    public Requisites(List<Requisite> value)
    {
        Value = value;
    }


  
}