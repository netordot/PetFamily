﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet.Species;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Domain.Pet.Breed;

public class Breed : SharedKernel.ValueObjects.Entity<BreedId>
{
    public string Name { get; private set; }
    public SpeciesId SpeciesId { get; private set; }

    private Breed(string name, BreedId id, SpeciesId speciesId) : base(id)
    {
        Name = name;
        SpeciesId = speciesId;
    }

    private Breed() : base(default)
    {
    
    }

    public static Result<Breed, Error> Create(string breedName, BreedId id, SpeciesId speciesId)
    {
        if (String.IsNullOrEmpty(breedName))
        {
            return Errors.General.ValueIsRequired(breedName);
        }
        //можно добавить проверку на пустой speciesId

        return new Breed(breedName, id, speciesId);
    }
}