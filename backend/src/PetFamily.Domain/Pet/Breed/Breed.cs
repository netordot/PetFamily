using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain;

public class Breed : Shared.Entity<BreedId>
{
    public string Name { get; set; }
    public Species Species { get; set; }

    private Breed(BreedId id) : base(id)
    {
        
    }

    private Breed(string name, Species species, BreedId id) : base(id)
    {
        Name = name;
        Species = species;
    }

    public static Result<Breed> Create(string breedName, Species species, BreedId id)
    {
        if (String.IsNullOrEmpty(breedName))
        {
            return Result.Failure<Breed>("BreedName cannot be empty.");
        }
        
        return new Breed(breedName, species, id);
    }
    
    
}