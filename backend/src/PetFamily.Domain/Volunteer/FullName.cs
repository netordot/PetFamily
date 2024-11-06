namespace PetFamily.Domain.Volunteer;

public record FullName
{
    public string Name { get;}
    public string? MiddleName { get;}
    public string? LastName { get;}

    private FullName()
    {
        
    }
    public FullName(string name, string middleName, string lastName)
    {
        Name = name;
        MiddleName = middleName;
        LastName = lastName;
    }
    
    
}