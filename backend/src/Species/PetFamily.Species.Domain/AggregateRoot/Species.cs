﻿using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Domain.Entities;

namespace PetFamily.Species.Domain.AggregateRoot;

public class Species : SharedKernel.ValueObjects.Entity<SpeciesId>
{
    public string Name { get; private set; }
    //TODO: инкапсулировать взаимодействие с листом
    public List<Breed>? Breeds { get; private set; }

    private Species() : base(default)
    {

    }
    private Species(SpeciesId id) : base(id)
    {

    }
    private Species(string name, List<Breed>? breeds, SpeciesId id) : base(id)
    {
        Name = name;
        Breeds = breeds;
    }

    public static Result<Species, Error> Create(string speciesName, List<Breed>? breeds, SpeciesId id)
    {
        if (string.IsNullOrEmpty(speciesName))
        {
            return Errors.General.ValueIsRequired(speciesName);
        }

        return new Species(speciesName, breeds, id);
    }

    public UnitResult<Error> AddBreed(Breed breed)
    {
        Breeds.Add(breed);
        return Result.Success<Error>();
    }

    public UnitResult<Error> DeleteBreed(Breed breed)
    {
        Breeds.Remove(breed);
        return Result.Success<Error>();
    }

}