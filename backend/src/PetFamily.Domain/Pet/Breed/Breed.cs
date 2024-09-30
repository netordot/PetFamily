using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.Pet.Breed;

public class Breed : Shared.Entity<BreedId>
{
    public string Name { get; private set; }
    
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