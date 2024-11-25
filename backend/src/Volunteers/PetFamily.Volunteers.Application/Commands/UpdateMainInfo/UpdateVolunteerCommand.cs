using PetFamily.Application.PetManagement.Commands.Volunteers.UpdateMainInfo;
using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Commands.UpdateMainInfo;

public record UpdateVolunteerCommand(UpdateVolunteerRequest dto, Guid id) : ICommand;