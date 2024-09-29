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
    
    public string Name { get; private set; }
    public SpeciesBreed SpeciesBreed { get;  private set; }
    public string Color { get; private set; }
    public string Description { get; private set; }
    public string HealthCondition { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; } 
    public Address Address { get; private set; }
    public PetStatus Status { get; private set; }

    public double Height { get; private set; }
    public double Weight { get; private set; } 
    public bool IsCastrated { get; private set; }
    public bool IsVaccinated { get; private set; }
    
    public DateTime DateOfBirth { get; }
    public DateTime CreatedAt { get; }
    public Requisites? Requisites { get; }
    public List<PetPhoto> Photos { get; private set; } = [];

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
            return Error.Validation("value.is.required", "phone number is required");
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