namespace PetFamily.Application.Volunteers.UpdateVolunteer;

public record UpdateVolunteerDto(
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