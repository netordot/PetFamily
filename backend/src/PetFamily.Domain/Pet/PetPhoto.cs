using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public class PetPhoto : Shared.Entity<PetPhotoId>
{
    public string Path { get; }
    public bool IsMain { get; }

    private PetPhoto(PetPhotoId id) : base(id)
    {
    }

    private  PetPhoto(string path, bool isMain, PetPhotoId petPhotoId) : base(petPhotoId)
    {
        Path = path;
        IsMain = isMain; 
    }

    public static Result<PetPhoto> Create(string path, bool isMain, PetPhotoId petPhotoId)
    {
        if (string.IsNullOrEmpty(path))
        {
            return Result.Failure<PetPhoto>("Path cannot be null or empty.");
        }
        
        return new PetPhoto(path, isMain, petPhotoId);  
    }
    
}