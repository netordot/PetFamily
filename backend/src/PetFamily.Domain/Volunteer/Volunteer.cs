namespace PetFamily.Domain.Volunteer;


public class Volunteer
{
    public Guid Id { get;}

    public string FullName { get; }

    public string Email { get; }
    public string Description { get; }
    public int Experience { get;}
    public int PhoneNumber { get; }
    public List<Pet> Pets { get; } = [];
    public List<Requisite> Requisites { get; } = [];
    public List<Social> Socials { get; } = [];

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