namespace PetFamily.Domain.Volunteer;

public class VolunteerDetails
{
    
    public IReadOnlyList<Social> SocialNetworks { get; }

    private VolunteerDetails()
    {
        
    }

    public VolunteerDetails(List<Social> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }

}