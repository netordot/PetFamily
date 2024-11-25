using PetFamily.Core.Dtos.PetManagement;
using PetFamily.CoreCore.Dtos.PetManagement;

namespace PetFamily.Volunteers.Presentation.Volunteers.Contracts
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
    List<SocialDto> SocialNetworks);

}
