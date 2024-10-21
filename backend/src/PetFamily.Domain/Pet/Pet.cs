using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Shared.Requisites;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Domain.Pet;

public class Pet : Shared.Entity<PetId>, ISoftDeletable
{
    // for ef core
    private Pet(PetId id) : base(id)
    {

    }

    public VolunteerId VolunteerId { get; private set; }
    public string Name { get; private set; }
    //временно nullable
    public SpeciesBreed SpeciesBreed { get; private set; }
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
    public DateTime DateOfBirth { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Requisites? Requisites { get; private set; }
    public IReadOnlyList<PetPhoto.PetPhoto>? Photos { get; private set; }

    private bool _isDeleted = false;

    private Pet(string name,
        SpeciesBreed speciesBreed,
        string color,
        string description,
        string healthCondition,
        PhoneNumber contactPhoneNumber,
        Address address,
        Requisites? requisites,
        PetStatus status,
        double height,
        double weight,
        bool isCastrated,
        bool isVaccinated,
        DateTime dateOfBirth,
        DateTime createdAt,
        List<PetPhoto.PetPhoto>? photos,
        //VolunteerId volunteerId,
        PetId id) : base(id)
    {
        Name = name;
        SpeciesBreed = speciesBreed;
        Color = color;
        Description = description;
        HealthCondition = healthCondition;
        PhoneNumber = contactPhoneNumber;
        Address = address;
        Requisites = requisites;
        Status = status;
        Height = height;
        Weight = weight;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        DateOfBirth = dateOfBirth;
        CreatedAt = createdAt;
        Photos = photos;
        //VolunteerId = volunteerId;
    }

    public static Result<Pet, Error> Create(string name,
        SpeciesBreed speciesBreed,
        string color,
        string description,
        string healthCondition,
        PhoneNumber contactPhoneNumbers,
        Address address,
        Requisites? requisites,
        PetStatus status,
        double height,
        double weight,
        bool isCastrated,
        bool isVaccinated,
        DateTime dateOfBirth,
        DateTime createdAt,
        List<PetPhoto.PetPhoto>? petPhotos,
        //VolunteerId volunteerId,
        PetId id)
    {
        if (String.IsNullOrEmpty(name))
        {
            return Errors.General.ValueIsRequired(name);
        }

        return new Pet(name,
            speciesBreed,
            color,
            description,
            healthCondition,
            contactPhoneNumbers,
            address,
            requisites,
            status,
            height,
            weight,
            isCastrated,
            isVaccinated,
            dateOfBirth,
            createdAt,
            petPhotos,
            //volunteerId,
            id);
    }

    public void Delete()
    {
        if (!_isDeleted)
            _isDeleted = true;
    }

    public void Restore()
    {
        if (_isDeleted)
        {
            _isDeleted = false;
        }
    }

    public UnitResult<Error> AddPhotos(List<PetPhoto.PetPhoto> photos)
    {
        Photos = photos;
        return Result.Success<Error>();
    }

}