using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.Pet.Species;

public class Species : Domain.Shared.Entity<SpeciesId>
{
    public string  Name { get; private set; }
    //TODO: инкапсулировать взаимодействие с листом
    public List<Breed.Breed>? Breeds { get; private set; }

    private Species() : base(default)
    {
        
    }
    private Species(SpeciesId id) : base(id)
    {

    }
    private Species(string name, List<Breed.Breed>? breeds, SpeciesId id) : base(id)
    {
        Name = name;
        Breeds = breeds;
    }

    public static Result<Species,Error> Create(string speciesName, List<Breed.Breed>? breeds, SpeciesId id)
    {
        if (String.IsNullOrEmpty(speciesName))
        {
            return Errors.General.ValueIsRequired(speciesName);
        }
        
        return new Species(speciesName, breeds, id);
    }

    public UnitResult<Error> AddBreed(Breed.Breed breed)
    {
        Breeds.Add(breed);
        return Result.Success<Error>();
    }

    public UnitResult<Error> DeleteBreed(Breed.Breed breed)
    {
        Breeds.Remove(breed);
        return Result.Success<Error>();
    }

}