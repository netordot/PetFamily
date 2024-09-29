namespace PetFamily.Domain.Volunteer;

public class VolunteerDetails
{
    
    public IReadOnlyList<Social> SocialNetworks ;

    private VolunteerDetails()
    {
        
    }

    public VolunteerDetails(List<Social> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }

    public static VolunteerDetails Create(List<Social> socialNetworks)
    {
        return new VolunteerDetails(socialNetworks);
    }
}