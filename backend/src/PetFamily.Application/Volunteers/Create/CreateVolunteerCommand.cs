using PetFamily.Application.Abstractions;
using PetFamily.Application.Volunteers.SharedDtos;

namespace PetFamily.Application.Volunteers.Create;

public record CreateVolunteerCommand(
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
    List<SocialNetworkDto> SocialNetworks
) : ICommand;