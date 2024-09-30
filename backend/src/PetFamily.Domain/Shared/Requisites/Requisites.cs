using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.Shared;

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