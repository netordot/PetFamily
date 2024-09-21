using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public record PetPhoto 
{
    public string Path { get; }
    public bool IsMain { get; }

    private  PetPhoto(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain; 
    }

    public static Result<PetPhoto> Create(string path, bool isMain)
    {
        if (path == null)
        {
            return Result.Failure<PetPhoto>("Path cannot be null");
        }
        return new PetPhoto(path, isMain);  
    }
}