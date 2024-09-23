using System.Security.AccessControl;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain;

public class Pet : Shared.Entity<PetId>
{ 
    // for ef core
    private Pet(PetId id) : base(id)
    {
        
    }
    
    public string Name { get; }
    public string Species { get; }
    public string Breed { get; }
    public string Color { get; }
    public string Description { get; }
    public string HealthCondition { get; }
    public string ContactPhoneNumber { get;} 
    public Adress Adress { get; }
    
    public PetStatus Status { get; }

    public double Height { get; }
    public double Weight { get; } 
    public bool IsCastrated { get; }
    public bool IsVaccinated { get;}
    
    public DateTime DateOfBirth { get; }
    public DateTime CreatedAt { get; }

    public Requisites? Requisites { get; }
    public List<PetPhoto> Photos { get; } = [];

}