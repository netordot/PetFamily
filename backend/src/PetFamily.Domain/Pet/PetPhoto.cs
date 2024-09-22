using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public class PetPhoto 
{
    public Guid Id { get; }
    public string Path { get; }
    public bool isMain { get; }

    private PetPhoto()
    {
        
    }

    private  PetPhoto(string path, bool isMain, Guid id)
    {
        Id = id;
        Path = path;
        this.isMain = isMain; 
    }

    public static Result<PetPhoto> Create(string path, bool isMain)
    {
        return new PetPhoto(path, isMain, Guid.NewGuid());  
    }
    
}