using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.SharedKernel.ValueObjects;

public class Requisite
{
    public string Title { get; private set; }
    public string Description { get; private set; }

    public Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public static Result<Requisite, Error> Create(string title, string description)
    {
        if (string.IsNullOrEmpty(title))
        {
            return Errors.General.ValueIsRequired("requisite title");
        }

        if (string.IsNullOrEmpty(description))
        {
            return Errors.General.ValueIsRequired("requisite description");
        }

        return new Requisite(title, description);
    }
}