using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;


public class Volunteer : Shared.Entity<VolunteerId>
{
    public VolunteerId Id { get; }
    public string FullName { get; }

    public string Email { get; }
    public string Description { get; }
    public int Experience { get;}
    public int PhoneNumber { get; }
    public List<Pet> Pets { get; } = [];
    
    public Requisites? Requisites { get; }
    public VolunteerDetails Details { get; }

    private Volunteer(VolunteerId id) : base(id)
    {
        
    }
    
    // добавить ctor и result 

    public int PetsRequireHome()
    {
        return Pets.Count(p => p.Status ==PetStatus.SearchesForHome);
    }

    public int PetsRequireHelp()
    {
        return Pets.Count(p => p.Status == PetStatus.NeedsHelp);
    }

    public int PetsAdopted()
    {
        return Pets.Count(p => p.Status ==PetStatus.Adopted);
    }
    

}