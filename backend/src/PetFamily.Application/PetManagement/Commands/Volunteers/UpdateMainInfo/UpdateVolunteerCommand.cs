using PetFamily.Core.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateMainInfo;

public record UpdateVolunteerCommand(UpdateVolunteerRequest dto, Guid id) : ICommand;