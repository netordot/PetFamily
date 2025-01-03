﻿using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.Enums;
using PetFamily.Volunteers.Domain.ValueObjects;
using System.Reflection.PortableExecutable;

namespace PetFamily.Volunteers.Domain.Entities;

public sealed class Pet : SoftDeletableEntity<PetId>
{
    private Pet(PetId id) : base(id)
    {

    }

    private Pet() : base(default)
    {

    }

    public VolunteerId VolunteerId { get; private set; }
    public string Name { get; private set; }
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
    public IReadOnlyList<Requisite> Requisites { get; private set; } = default!;
    public IReadOnlyList<PetPhoto>? Photos { get; private set; }
    public Position Position { get; private set; }

    private Pet(string name,
        SpeciesBreed speciesBreed,
        string color,
        string description,
        string healthCondition,
        PhoneNumber contactPhoneNumber,
        Address address,
        List<Requisite> requisites,
        PetStatus status,
        double height,
        double weight,
        bool isCastrated,
        bool isVaccinated,
        DateTime dateOfBirth,
        DateTime createdAt,
        List<PetPhoto>? photos,
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
    }

    public static Result<Pet, Error> Create(string name,
        SpeciesBreed speciesBreed,
        string color,
        string description,
        string healthCondition,
        PhoneNumber contactPhoneNumbers,
        Address address,
        List<Requisite> requisites,
        PetStatus status,
        double height,
        double weight,
        bool isCastrated,
        bool isVaccinated,
        DateTime dateOfBirth,
        DateTime createdAt,
        List<PetPhoto>? petPhotos,
        PetId id)
    {
        if (string.IsNullOrEmpty(name))
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
            id);
    }

    public void SetStatus(PetStatus status)
    {
        Status = status;
    }

    public UnitResult<Error> SetPosition(Position number)
    {
        Position = number;
        return Result.Success<Error>();
    }

    public UnitResult<Error> UploadPhotos(List<PetPhoto> photos)
    {
        Photos = photos;
        return Result.Success<Error>();
    }

    public void DeletePhoto(PetPhoto photo)
    {
        var photos = Photos.ToList();
        photos.Remove(photo);
        Photos = photos;
    }

    // дополнительно протестировать и поработать над тем, чтобы возвращалась сначала главная фотка
    public UnitResult<Error> SetMainPhoto(PetPhoto mainPhoto)
    {
        if (Photos == null)
        {
            return Errors.General.NotFound();
        }
        var photos = Photos.Select(p => PetPhoto.Create(p.Path, false).Value).ToList();
        var targetPhotoIndex = photos.IndexOf(Photos.FirstOrDefault(p => p.Path == mainPhoto.Path));
        photos[targetPhotoIndex] = mainPhoto;

        Photos = photos;

        return Result.Success<Error>();
    }

    public UnitResult<Error> AddNewPhotos(List<PetPhoto> photos)
    {
        var newList = Photos.Concat(photos);
        Photos = newList.ToList();
        return Result.Success<Error>();
    }

    public UnitResult<Error> UpdatePet
        (string name,
        SpeciesBreed speciesBreed,
        string color,
        string description,
        string healthCondition,
        PhoneNumber contactPhoneNumbers,
        Address address,
        List<Requisite> requisites,
        PetStatus status,
        double height,
        double weight,
        bool isCastrated,
        bool isVaccinated,
        DateTime dateOfBirth)
    {
        Name = name;
        SpeciesBreed = speciesBreed;
        Color = color;
        Description = description;
        HealthCondition = healthCondition;
        PhoneNumber = contactPhoneNumbers;
        Address = address;
        Requisites = requisites;
        Height = height;
        Weight = weight;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        DateOfBirth = dateOfBirth;

        return Result.Success<Error>();
    }

    public UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();
        if (newPosition.IsFailure)
        {
            return newPosition.Error;
        }

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public UnitResult<Error> MoveBack()
    {
        var newPosition = Position.Backward();
        if (newPosition.IsFailure)
        {
            return newPosition.Error;
        }

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public void MovePosition(Position position)
    {
        Position = position;
    }

    public override void Restore()
    {
        base.Restore(); 
    }

    public override void Delete()
    {
        base.Delete();
    }
}