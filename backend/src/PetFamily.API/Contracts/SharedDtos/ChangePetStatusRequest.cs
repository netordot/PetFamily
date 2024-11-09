using PetFamily.Application.Dtos;

namespace PetFamily.API.Contracts.SharedDtos
{
    public record ChangePetStatusRequest(PetStatusDto Status);
    
}
