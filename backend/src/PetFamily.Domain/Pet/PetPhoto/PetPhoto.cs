using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.Pet.PetPhoto;

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
        // �������� �����-�� ���������
        return new PetPhoto(path, isMain, petPhotoId);
    }

}