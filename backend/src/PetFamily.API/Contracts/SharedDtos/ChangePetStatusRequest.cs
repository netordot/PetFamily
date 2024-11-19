using PetFamily.Core.Dtos.PetManagement;

namespace PetFamily.API.Contracts.SharedDtos
{
    public record ChangePetStatusRequest(PetStatusDto Status);
    
}
