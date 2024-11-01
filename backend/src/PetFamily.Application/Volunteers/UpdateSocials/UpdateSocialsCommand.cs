using PetFamily.Application.Abstractions;
using PetFamily.Application.Volunteers.SharedDtos;

namespace PetFamily.Application.Volunteers.UpdateSocials;

public record UpdateSocialsCommand(Guid Id, SocialsListDto Dto) :ICommand;