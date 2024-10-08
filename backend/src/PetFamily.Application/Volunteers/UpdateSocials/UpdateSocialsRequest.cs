using PetFamily.Application.Volunteers.SharedDtos;

namespace PetFamily.Application.Volunteers.UpdateSocials;

public record UpdateSocialsRequest(Guid Id, SocialsListDto Dto);