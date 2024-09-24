using System.Security.AccessControl;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain;

public class Pet : Shared.Entity<PetId>
{ 
    // for ef core
    private Pet(PetId id) : base(id)
    {
        
    }
    
    public string Name { get; }
    public SpeciesBreed SpeciesBreed { get;  }
    public string Color { get; }
    public string Description { get; }
    public string HealthCondition { get; }
    public PhoneNumbers PhoneNumbers { get; } 
    public Adress Adress { get; }
    public PetStatus Status { get; }

    public double Height { get; }
    public double Weight { get; } 
    public bool IsCastrated { get; }
    public bool IsVaccinated { get;}
    
    public DateTime DateOfBirth { get; }
    public DateTime CreatedAt { get; }

    public Requisites? Requisites { get; }
    public List<PetPhoto> Photos { get; } = [];

    private Pet(string name, SpeciesBreed speciesBreed, string color, string description, string healthCondition,
        PhoneNumbers contactPhoneNumbers, Adress adress, PetStatus status, double height, double weight, 
        bool isCastrated, bool isVaccinated,DateTime dateOfBirth, DateTime createdAt, List<PetPhoto> photos, PetId id) : base(id)
    {
        Name = name;
        SpeciesBreed = speciesBreed;
        Color = color;
        Description = description;
        HealthCondition = healthCondition;
        PhoneNumbers = contactPhoneNumbers;
        Adress = adress;
        Status = status;
        Height = height;
        Weight = weight;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        DateOfBirth = dateOfBirth;
        CreatedAt = createdAt;
        Photos = photos;
    }

    public static Result<Pet> Create(string name, SpeciesBreed speciesBreed, string color, string description,
        string healthCondition, PhoneNumbers contactPhoneNumbers, Adress adress, PetStatus status, double height, double weight,
        bool isCastrated, bool isVaccinated, DateTime dateOfBirth, DateTime createdAt, List<PetPhoto> photos, PetId id)
    {
        if (String.IsNullOrEmpty(name))
        {
            return Result.Failure<Pet>("Name is required");
        }

        if (contactPhoneNumbers == null)
        {
            return Result.Failure<Pet>("Contact phone number is required");
        }

        if (adress == null)
        {
            return Result.Failure<Pet>("Adress is required");
        }

        if (status == null)
        {
            return Result.Failure<Pet>("Status is required");
        }
        
        return new Pet(name, speciesBreed, color, description, healthCondition, contactPhoneNumbers, 
            adress, status, height, weight, isCastrated, isVaccinated, dateOfBirth, createdAt,photos, id);
    }

}