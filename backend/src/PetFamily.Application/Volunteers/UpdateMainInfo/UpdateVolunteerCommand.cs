using PetFamily.Application.Abstractions;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public record UpdateVolunteerCommand(UpdateVolunteerRequest dto, Guid id) : ICommand;