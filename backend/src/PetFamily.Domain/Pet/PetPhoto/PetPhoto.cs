using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.Pet.PetPhoto;

public record PetPhoto
{
    public FilePath Path { get; private set; }
    public bool IsMain { get; private set; }

    private PetPhoto() 
    {
    }

    private PetPhoto(FilePath path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public static Result<PetPhoto, Error> Create(FilePath path, bool isMain)
    {
        // �������� �����-�� ���������
        return new PetPhoto(path, isMain);
    }

}