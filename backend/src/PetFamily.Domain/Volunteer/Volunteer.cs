using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.PhoneNumber;

namespace PetFamily.Domain.Volunteer;
public class Volunteer : Shared.Entity<VolunteerId>
{
    public FullName Name { get; private set; }
    public Requisites? Requisites { get; }
    public VolunteerDetails? Details { get; }
    public Address Address { get;  }
    
    public Email Email { get; }
    public string Description { get; }
    public int Experience { get;}
    public PhoneNumber Number { get; }
    public List<Pet>? Pets { get; } = [];
    
    private Volunteer (VolunteerId id) : base(id)
    {
        
    }

    public Volunteer(FullName name, 
        Email email, 
        string description,
        int experience,
        PhoneNumber  phoneNumber, 
        List<Pet> pets,
        Address address,
        Requisites requisites,
        VolunteerDetails details,
        VolunteerId id) : base(id)
    {
        Name = name;
        Email = email;
        Description = description;
        Experience = experience;
        Number = phoneNumber;
        Pets = pets;
        Address = address;
        Requisites = requisites;
        Details = details;
    }
    
    public int PetsRequireHome() => Pets.Count(p => p.Status ==PetStatus.SearchesForHome);
    public int PetsRequireHelp() => Pets.Count(p => p.Status == PetStatus.NeedsHelp);
    public int PetsAdopted() => Pets.Count(p => p.Status == PetStatus.Adopted);
    
    public static Result<Volunteer,Error> Create(FullName name,
        Email emails, 
        string description, 
        int experience,
        PhoneNumber phoneNumber,
        List<Pet>? pets, 
        Address address, 
        Requisites requisites, 
        VolunteerDetails details, 
        VolunteerId id)
    {
        if (name == null)
        {
            return Error.Validation("value.is.required", "name is required");
        }

        if (emails == null)
        {
            return Error.Validation("value.is.required", "email is required");
        }

        if (phoneNumber == null)
        {
            return Error.Validation("value.is.required", "phonenumber is required");

        }
        
        return new Volunteer(name, 
            emails,
            description, 
            experience, 
            phoneNumber,
            pets,
            address,
            requisites,
            details,
            id);
    }
}