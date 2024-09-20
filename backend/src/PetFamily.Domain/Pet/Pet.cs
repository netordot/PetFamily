using System.Security.AccessControl;

namespace PetFamily.Domain;

public class Pet
{ 
    // for ef core
    private Pet()
    {
        
    }
    public Guid Id { get; }
    
    public string Name { get; }
    public string Species { get; }
    public string Breed { get; }
    public string Color { get; }
    public string Description { get; }
    public string HealthCondition { get; }
    public string ContactPhoneNumber { get;} 
    public string Adress { get; }
    
    public PetStatus Status { get; }

    public double Height { get; }
    public double Weight { get; } 
    public bool IsCastrated { get; }
    public bool IsVaccinated { get;}
    
    public DateTime DateOfBirth { get; }
    public DateTime CreatedAt { get; }

    public List<Requisite> Requisites { get; } = [];
    public List<PetPhoto> Photos { get; } = [];

}