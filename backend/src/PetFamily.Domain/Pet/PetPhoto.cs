using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public class PetPhoto : ValueObject
{
    public string Path { get; }
    public bool isMain { get; }

    private  PetPhoto(string path, bool isMain)
    {
        Path = path;
        this.isMain = isMain; 
    }

    public static Result<PetPhoto> Create(string path, bool isMain)
    {
        return new PetPhoto(path, isMain);  
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Path;
        yield return isMain;
    }
}