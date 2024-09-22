namespace PetFamily.Domain.Volunteer;

public class VolunteerDetails
{
    private readonly List<Social> _socialNetworks;
    
    public IReadOnlyList<Social> SocialNetworks => _socialNetworks;
}