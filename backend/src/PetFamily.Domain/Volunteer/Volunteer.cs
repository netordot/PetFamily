using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;
public class Volunteer : Shared.Entity<VolunteerId>
{
    public FullName Name { get; private set; }
    public Requisites? Requisites { get; }
    public VolunteerDetails Details { get; }
    public Adress Adress { get;  }
    
    public string Email { get; }
    public string Description { get; }
    public int Experience { get;}
    public string PhoneNumber { get; }
    public List<Pet> Pets { get; } = [];
    
    private Volunteer (VolunteerId id) : base(id)
    {
        
    }

    public Volunteer(FullName name, string email, string description, int experience,string  phoneNumber, 
        List<Pet> pets, Adress adress, Requisites requisites, VolunteerDetails details, VolunteerId id) : base(id)
    {
        Name = name;
        Email = email;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
        Pets = pets;
        Adress = adress;
        Requisites = requisites;
        Details = details;
        
    }
    
    public int PetsRequireHome() => Pets.Count(p => p.Status ==PetStatus.SearchesForHome);
    public int PetsRequireHelp() => Pets.Count(p => p.Status == PetStatus.NeedsHelp);
    public int PetsAdopted() => Pets.Count(p => p.Status == PetStatus.Adopted);
    
    public static Result<Volunteer> Create(FullName name, string email, string description, int experience,
        string phoneNumber, List<Pet> pets, Adress adress, Requisites requisites, VolunteerDetails details, VolunteerId id)
    {
        if (name == null)
        {
            return Result.Failure<Volunteer>("Name is required");
        }

        if (email == null)
        {
            return Result.Failure<Volunteer>("Email is required");
        }

        if (string.IsNullOrEmpty(phoneNumber))
        {
            return Result.Failure<Volunteer>("Phone number is required");
        }
        
        return new Volunteer(name, email, description, experience, phoneNumber, pets, adress, requisites, details, id);
    }
}