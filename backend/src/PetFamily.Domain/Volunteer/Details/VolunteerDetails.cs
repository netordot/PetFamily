using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Domain.Volunteer;

public class VolunteerDetails
{
    public IReadOnlyList<Social> SocialNetworks { get; }

    public VolunteerDetails(List<Social> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
}