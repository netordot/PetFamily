using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Domain.Volunteer;

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