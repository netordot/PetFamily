using System.Reflection.Metadata.Ecma335;

namespace PetFamily.Domain.Shared;

public record Adress
{
    public string City { get; private set; }
    public string  Street { get; private set; }
    public int  BuildingNumber { get; private set; }
    public int?  CropsNumber { get; private  set; }

    public Adress(string city, string street, int buildingNumber, int cropsNumber)
    {
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
        CropsNumber = cropsNumber;
    }
}