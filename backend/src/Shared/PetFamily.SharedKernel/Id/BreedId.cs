﻿namespace PetFamily.SharedKernel.Id;

public record BreedId
{
    public Guid Value { get; }

    private BreedId(Guid value)
    {
        Value = value;
    }

    private BreedId()
    {

    }

    public static BreedId NewBreedId => new(Guid.NewGuid());
    public static BreedId Empty => new(Guid.Empty);
    public static BreedId Create(Guid id) => new(id);
}