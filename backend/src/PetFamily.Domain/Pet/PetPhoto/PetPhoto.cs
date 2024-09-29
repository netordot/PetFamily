using PetFamily.Domain.Shared;

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
            return Result<PetPhoto>.Failure("Path cannot be null or empty.");
        }
        var photo = new PetPhoto(path, isMain, petPhotoId);
        
        return Result<PetPhoto>.Success(photo);  
    }
    
}