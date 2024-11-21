using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Other;

namespace PetFamily.SharedKernel.ValueObjects;

public record Social
{
    public Social(string name, string link)
    {
        Name = name;
        Link = link;
    }

    private Social()
    {

    }

    public string Name { get; }
    public string Link { get; }

    public static Result<Social, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(link))
        {
            return Errors.General.ValueIsRequired("Link");
        }

        return new Social(name, link);
    }
}