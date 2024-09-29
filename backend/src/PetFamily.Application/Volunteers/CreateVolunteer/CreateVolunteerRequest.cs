using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    string FirstName,
    string MiddleName ,
    string LastName,
    string Email,
    string PhoneNumber,
    
    int Experience,
    string Description,
    
    string City, 
    string Street, 
    int BuildingNumber,
    int CorpsNumber,
    
    List<RequisiteDto> Requisites,
    List<SocialNetworkDto> SocialNetworks
);

public record RequisiteDto(string Title, string Description);
public record SocialNetworkDto(string Name, string Link);