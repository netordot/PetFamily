﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;

namespace PetFamily.Domain.Volunteer;

public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    private bool _isDeleted;
    public FullName Name { get; private set; }
    public Requisites? Requisites { get; private set; }
    public VolunteerDetails? Details { get; private set; }
    public Address Address { get; private set; }

    public Email Email { get; private set; }
    public string Description { get; private set; }
    public int Experience { get; private set; }
    public PhoneNumber Number { get; private set; }
    public List<Pet.Pet>? Pets { get; private set; }

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(FullName name,
        Email email,
        string description,
        int experience,
        PhoneNumber phoneNumber,
        List<Pet.Pet> pets,
        Address address,
        Requisites requisites,
        VolunteerDetails details,
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
        Details = details;
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
        Requisites requisites,
        VolunteerDetails details,
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

    public void UpdateSocials(VolunteerDetails newSocials)
    {
        Details = newSocials;
    }

    public void UdpateRequisites(Requisites requisites)
    {
        Requisites = requisites;
    }

    public void Delete()
    {
        if(!_isDeleted)
            _isDeleted = true;
    }

    public void Restore()
    {
        if(_isDeleted)
        {
            _isDeleted= false;
        }
    }

    public UnitResult<Error> AddPet(Pet.Pet pet)
    {
        Pets.Add(pet);
        return Result.Success<Error>();
    }
}