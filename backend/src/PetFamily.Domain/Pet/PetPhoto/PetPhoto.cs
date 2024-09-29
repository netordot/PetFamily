using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain;

public class PetPhoto : Shared.Entity<PetPhotoId>
{
    public string Path { get; private set; }
    public bool IsMain { get; private set; }

    private PetPhoto(PetPhotoId id) : base(id)
    {
    }

    private  PetPhoto(string path, bool isMain, PetPhotoId petPhotoId) : base(petPhotoId)
    {
        Path = path;
        IsMain = isMain; 
    }

    public static Result<PetPhoto,Error> Create(string path, bool isMain, PetPhotoId petPhotoId)
    {
        if (string.IsNullOrEmpty(path))
        {
            return Errors.General.ValueIsRequired("");
        }
        
        return    new PetPhoto(path, isMain, petPhotoId);
    }
    
}