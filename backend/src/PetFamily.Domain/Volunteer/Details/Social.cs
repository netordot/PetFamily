using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.Volunteer.Details;

public record Social
{
    
    public Social(string name, string link)
    {
        Name = name;
        Link = link;
    }
    public string Name { get;}
    public string Link { get;}
    
   public static Result<Social,Error> Create(string name, string link)
   {
       if (String.IsNullOrWhiteSpace(link))
       {
           return Errors.General.ValueIsRequired("Link");
       }
       
       return new Social(name, link);
   }
}