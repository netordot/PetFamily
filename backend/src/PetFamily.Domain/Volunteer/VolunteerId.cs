using System.Net;

namespace PetFamily.Domain.Volunteer;

public class VolunteerId
{
    public Guid Value { get; }
    public VolunteerId(Guid value)
    {
        Value = value;
    }
    
    public static VolunteerId NewVolunteerId => new(Guid.NewGuid());
    public static VolunteerId Empty => new(Guid.Empty); 
    public static VolunteerId Create(Guid id) => new(id);
}