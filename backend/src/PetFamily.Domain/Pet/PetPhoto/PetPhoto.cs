using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain;

public class PetPhoto : Shared.Entity<PetPhotoId>
{
    public FilePath Path { get; private set; }
    public bool IsMain { get; private set; }

    private PetPhoto(PetPhotoId id) : base(id)
    {
    }

    private PetPhoto(FilePath path, bool isMain, PetPhotoId petPhotoId) : base(petPhotoId)
    {
        Path = path;
        IsMain = isMain;
    }

    public static Result<PetPhoto, Error> Create(FilePath path, bool isMain, PetPhotoId petPhotoId)
    {
        // возможно какая-то валидация
        return new PetPhoto(path, isMain, petPhotoId);
    }

}