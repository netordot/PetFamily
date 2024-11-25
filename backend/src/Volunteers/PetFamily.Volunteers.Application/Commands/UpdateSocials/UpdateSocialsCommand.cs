using PetFamily.Core.Abstractions;
using PetFamily.CoreCore.Dtos.PetManagement;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateSocials;

public record UpdateSocialsCommand(Guid Id, List<SocialDto> socialsList) : ICommand;