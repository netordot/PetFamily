using System.Security.AccessControl;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.PhoneNumber;

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
    public PhoneNumber PhoneNumber { get; } 
    public Address Address { get; }
    public PetStatus Status { get; }

    public double Height { get; }
    public double Weight { get; } 
    public bool IsCastrated { get; }
    public bool IsVaccinated { get;}
    
    public DateTime DateOfBirth { get; }
    public DateTime CreatedAt { get; }
    public Requisites? Requisites { get; }
    public List<PetPhoto> Photos { get; } = [];

    private Pet(string name, 
        SpeciesBreed speciesBreed,
        string color,
        string description, 
        string healthCondition,
        PhoneNumber contactPhoneNumber,
        Address address,
        PetStatus status, 
        double height, 
        double weight, 
        bool isCastrated,
        bool isVaccinated,
        DateTime dateOfBirth, 
        DateTime createdAt, 
        List<PetPhoto> photos, 
        PetId id) : base(id)
    {
        Name = name;
        SpeciesBreed = speciesBreed;
        Color = color;
        Description = description;
        HealthCondition = healthCondition;
        PhoneNumber = contactPhoneNumber;
        Address = address;
        Status = status;
        Height = height;
        Weight = weight;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        DateOfBirth = dateOfBirth;
        CreatedAt = createdAt;
        Photos = photos;
    }

    public static Result<Pet, Error> Create(string name,
        SpeciesBreed speciesBreed,
        string color,
        string description,
        string healthCondition,
        PhoneNumber contactPhoneNumbers,
        Address address,
        PetStatus status,
        double height,
        double weight,
        bool isCastrated,
        bool isVaccinated,
        DateTime dateOfBirth,
        DateTime createdAt,
        List<PetPhoto> photos,
        PetId id)
    {
        if (String.IsNullOrEmpty(name))
        {
            return Errors.General.ValueIsRequired(name);
        }

        if (contactPhoneNumbers == null)
        {
            return Error.Validation("value.is.required", "phonenumber is required");
        }

        if (address == null)
        {
            return Error.Validation("value.is.required", "address is required");

        }

        if (status == null)
        {
            return Error.Validation("value.is.required", "status is required");
        }
        
        return new Pet(name, 
            speciesBreed,
            color,
            description,
            healthCondition,
            contactPhoneNumbers, 
            address, 
            status, 
            height,
            weight,
            isCastrated,
            isVaccinated, 
            dateOfBirth, 
            createdAt,
            photos, 
            id);
    }

}