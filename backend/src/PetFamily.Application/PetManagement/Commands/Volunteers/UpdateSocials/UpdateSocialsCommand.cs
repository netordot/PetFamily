using PetFamily.Application.PetManagement.Commands.Volunteers.SharedDtos;
using PetFamily.Core.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateSocials;

public record UpdateSocialsCommand(Guid Id, SocialsListDto Dto) : ICommand;