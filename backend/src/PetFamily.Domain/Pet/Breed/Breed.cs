using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain;

public class Breed : Shared.Entity<BreedId>
{
    public string Name { get; set; }

    private Breed(BreedId id) : base(id)
    {
        
    }

    private Breed(string name,  BreedId id) : base(id)
    {
        Name = name;
    }

    public static Result<Breed,Error> Create(string breedName,  BreedId id)
    {
        if (String.IsNullOrEmpty(breedName))
        {
            return Errors.General.ValueIsRequired(breedName);
        }
        
        return new Breed(breedName, id);
    }
    
    
}