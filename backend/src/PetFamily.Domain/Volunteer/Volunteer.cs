using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using System.Runtime.InteropServices.ObjectiveC;

namespace PetFamily.Domain.Volunteer;

public class Volunteer : SharedKernel.ValueObjects.Entity<VolunteerId>, ISoftDeletable
{
    private bool _isDeleted;
    public FullName Name { get; private set; }
    public IReadOnlyList<Requisite>? Requisites { get; private set; } = default!;
    public IReadOnlyList<Social>? Socials { get; private set; } = default!;
    public Address Address { get; private set; }

    public Email Email { get; private set; }
    public string Description { get; private set; }
    public int Experience { get; private set; }
    public PhoneNumber Number { get; private set; }
    public List<Pet.Pet>? Pets { get; private set; }

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    private Volunteer() : base(default)
    {
        
    }

    public Volunteer(FullName name,
        Email email,
        string description,
        int experience,
        PhoneNumber phoneNumber,
        List<Pet.Pet> pets,
        Address address,
        List<Requisite> requisites,
        List<Social> details,
        VolunteerId id) : base(id)
    {
        Name = name;
        Email = email;
        Description = description;
        Experience = experience;
        Number = phoneNumber;
        Pets = pets;
        Address = address;
        Requisites = requisites;
        Socials = details;
    }

    public int PetsRequireHome() => Pets.Count(p => p.Status == PetStatus.SearchesForHome);
    public int PetsRequireHelp() => Pets.Count(p => p.Status == PetStatus.NeedsHelp);
    public int PetsAdopted() => Pets.Count(p => p.Status == PetStatus.Adopted);

    public static Result<Volunteer, Error> Create(FullName name,
        Email emails,
        string description,
        int experience,
        PhoneNumber phoneNumber,
        List<Pet.Pet>? pets,
        Address address,
        List<Requisite> requisites,
        List<Social> details,
        VolunteerId id)
    {
        if (name == null)
        {
            return Error.Validation("value.is.required", "name is required");
        }

        if (emails == null)
        {
            return Error.Validation("value.is.required", "email is required");
        }

        if (phoneNumber == null)
        {
            return Error.Validation("value.is.required", "phonenumber is required");
        }

        return new Volunteer(name,
            emails,
            description,
            experience,
            phoneNumber,
            pets,
            address,
            requisites,
            details,
            id);
    }

    public void UpdateMainInfo(
        FullName name,
        Email emails,
        string description,
        int experience,
        PhoneNumber phoneNumber,
        Address address)
    {
        Name = name;
        Email = emails;
        Description = description;
        Experience = experience;
        Number = phoneNumber;
        Address = address;
    }

    public void UpdateSocials(List<Social> newSocials)
    {
        Socials = newSocials;
    }

    public void UdpateRequisites(List<Requisite> requisites)
    {
        Requisites = requisites;
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

    public UnitResult<Error> AddPet(Pet.Pet pet)
    {
        var serialNumber = Position.Create(Pets.Count + 1);
        pet.SetPosition(serialNumber.Value);

        Pets.Add(pet);
        return Result.Success<Error>();
    }

    public void HardDeletePet (Pet.Pet pet)
    {
        Pets.Remove(pet);
    }

    public Result<Pet.Pet, Error> GetPetById(Guid petId)
    {
        var pet = Pets.FirstOrDefault(p => p.Id == PetId.Create(petId));
        if (pet == null)
        {
            return Errors.General.NotFound(petId);
        }

        return pet;
    }

    public UnitResult<Error> MovePet(Pet.Pet pet, Position position)
    {
        var currentPosition = pet.Position;

        if (currentPosition == position || Pets.Count == 1)
        {
            return Result.Success<Error>();
        }

        var adjustedPosition = SetPositionIfOutOfRange(position);
        if (adjustedPosition.IsFailure)
        {
            return adjustedPosition.Error;
        }

        var newPosition = adjustedPosition.Value;

        var moveResult = AdjutPositionsBetween(currentPosition, newPosition);
        if (moveResult.IsFailure)
        {
            return moveResult.Error;
        }

        pet.MovePosition(position);

        return Result.Success<Error>();
    }

    private UnitResult<Error> AdjutPositionsBetween(Position currentPosition, Position newPosition)
    {
        if (newPosition.Value < currentPosition.Value)
        {
            var petsToMove = Pets.Where(p => p.Position.Value >= newPosition.Value
                                             && p.Position.Value < currentPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveForward();
                if (result.IsFailure)
                {
                    return result.Error;
                }
            }
        }

        else if (newPosition.Value > currentPosition.Value)
        {
            var petsToMove = Pets.Where(p => p.Position.Value > currentPosition.Value
                                             && p.Position.Value <= newPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBack();
                if (result.IsFailure)
                {
                    return result.Error;
                }
            }
        }

        return Result.Success<Error>();
    }

    private Result<Position, Error> SetPositionIfOutOfRange(Position newPosition)
    {
        if (newPosition.Value <= Pets.Count())
        {
            return newPosition;
        }

        var lastPosition = Position.Create(Pets.Count() - 1);
        if (lastPosition.IsFailure)
        {
            return lastPosition.Error;
        }

        return lastPosition.Error;
    }
}