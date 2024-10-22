namespace PetFamily.Domain.Volunteer.Details;

public class VolunteerDetails
{

    public VolunteerDetails()
    {
        
    }
    public IReadOnlyList<Social> SocialNetworks { get; }


    public VolunteerDetails(IEnumerable<Social> socialNetworks)
    {
        SocialNetworks = socialNetworks.ToList();
    }
}