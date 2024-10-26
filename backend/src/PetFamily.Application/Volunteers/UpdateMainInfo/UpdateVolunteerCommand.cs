namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public record UpdateVolunteerCommand(UpdateVolunteerRequest dto, Guid id);