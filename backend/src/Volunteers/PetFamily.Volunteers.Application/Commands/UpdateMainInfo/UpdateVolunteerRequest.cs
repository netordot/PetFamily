namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateMainInfo;

public record UpdateVolunteerRequest(
    string FirstName,
    string MiddleName,
    string LastName,
    string Description,
    int Experience,
    string PhoneNumber,
    string Email,
    string City,
    string Street,
    int BuildingNumber,
    int? CorpsNumber);