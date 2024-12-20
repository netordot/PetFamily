﻿namespace PetFamily.Volunteers.Domain.ValueObjects;

public record Address
{
    private Address()
    {

    }

    public string City { get; }
    public string Street { get; }
    public int BuildingNumber { get; }
    public int? CropsNumber { get; }

    public Address(string city, string street, int buildingNumber, int? cropsNumber)
    {
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
        CropsNumber = cropsNumber;
    }
}