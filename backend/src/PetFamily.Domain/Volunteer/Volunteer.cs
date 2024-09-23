using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;
public class Volunteer : Shared.Entity<VolunteerId>
{
    public FullName Name { get; private set; }
    public Requisites? Requisites { get; }
    public VolunteerDetails Details { get; }
    public Adress Adress { get;  }
    
    public Emails Emails { get; }
    public string Description { get; }
    public int Experience { get;}
    public PhoneNumbers Numbers { get; }
    public List<Pet> Pets { get; } = [];
    
    private Volunteer (VolunteerId id) : base(id)
    {
        
    }

    public Volunteer(FullName name, Emails email, string description, int experience,PhoneNumbers  phoneNumber, 
        List<Pet> pets, Adress adress, Requisites requisites, VolunteerDetails details, VolunteerId id) : base(id)
    {
        Name = name;
        Emails = email;
        Description = description;
        Experience = experience;
        Numbers = phoneNumber;
        Pets = pets;
        Adress = adress;
        Requisites = requisites;
        Details = details;
        
    }
    
    public int PetsRequireHome() => Pets.Count(p => p.Status ==PetStatus.SearchesForHome);
    public int PetsRequireHelp() => Pets.Count(p => p.Status == PetStatus.NeedsHelp);
    public int PetsAdopted() => Pets.Count(p => p.Status == PetStatus.Adopted);
    
    public static Result<Volunteer> Create(FullName name, Emails emails, string description, int experience,
        PhoneNumbers phoneNumber, List<Pet> pets, Adress adress, Requisites requisites, VolunteerDetails details, VolunteerId id)
    {
        if (name == null)
        {
            return Result.Failure<Volunteer>("Name is required");
        }

        if (emails == null)
        {
            return Result.Failure<Volunteer>("Email is required");
        }

        if (phoneNumber == null)
        {
            return Result.Failure<Volunteer>("Phone number is required");
        }
        
        return new Volunteer(name, emails, description, experience, phoneNumber, pets, adress, requisites, details, id);
    }
}