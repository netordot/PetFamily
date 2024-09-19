namespace PetFamily.Domain.Volunteer;


public class Volunteer
{
    public Guid Id { get;}

    public string FullName { get; }

    public string Email { get; }
    public string Description { get; }
    public int Experience { get;}
    public int PhoneNumber { get; }

    public int PetsRequireHome { get; }
    public int PetsAdopted { get; } 
    public int PetsOnTreatment { get; }

    public List<Pet> Pets { get; } = [];
    public List<Requisite> Requisites { get; } = [];
    public List<Social> Socials { get; } = [];
    



}