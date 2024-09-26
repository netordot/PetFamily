using PetFamily.Domain.Shared;

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

    public static Result<Breed> Create(string breedName,  BreedId id)
    {
        if (String.IsNullOrEmpty(breedName))
        {
            return Result<Breed>.Failure("BreedName cannot be empty.");
        }
        
        return new Breed(breedName, id);
    }
    
    
}