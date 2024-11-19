using PetFamily.Application.PetManagement.Commands.Volunteers.SharedDtos;

namespace PetFamily.API.Contracts
{
    public record CreateVolunteerRequest
    (
    string FirstName,
    string MiddleName,
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
    List<SocialNetworkDto> SocialNetworks);

}
