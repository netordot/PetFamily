using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain;

public class Species : Domain.Shared.Entity<SpeciesId>
{
    public string  Name { get; private set; }
    public List<Breed> Breeds { get; set; }

    private Species(SpeciesId id) : base(id)
    {
        
    }

    private Species(string name, List<Breed> breeds, SpeciesId id) : base(id)
    {
        Name = name;
        Breeds = breeds;
    }

    public static Result<Species,Error> Create(string speciesName, List<Breed> breeds, SpeciesId id)
    {
        if (String.IsNullOrEmpty(speciesName))
        {
            return Errors.General.ValueIsRequired(speciesName);
        }
        
        return new Species(speciesName, breeds, id);
    }
}