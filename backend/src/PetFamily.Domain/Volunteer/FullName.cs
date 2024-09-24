namespace PetFamily.Domain.Volunteer;


public record FullName
{
    public string Name { get; private set; }
    public string? MiddleName { get; private set; }
    public string? LastName { get; private set; }

    public FullName(string name, string middleName, string lastName)
    {
        Name = name;
        MiddleName = middleName;
        LastName = lastName;
    }
    
    
}