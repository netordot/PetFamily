using PetFamily.Domain.Shared;

namespace PetFamily.Domain;

public class Requisite
{
    public string Title { get; }
    public string Description { get;}

    public Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public static Requisite Create(string title, string description)
    {
       return new Requisite(title, description);
    }
}