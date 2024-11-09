using PetFamily.Application.Dtos;
using PetFamily.Application.Volunteers.SharedDtos;
using RequisiteDto = PetFamily.Application.Volunteers.SharedDtos.RequisiteDto;

namespace PetFamily.API.Contracts
{
    public record UpdatePetMainInfoRequest(
         string Name,
        Guid SpeciesId,
        Guid BreedId,
        string PhoneNumber,
        string Color,
        string Description,
        string HealthCondition,
        string City,
        string Street,
        int BuildingNumber,
        int CorpsNumber,
        // пока что статус будет доменным, когда будет фронт, избавиться от доменна в реквесте
        Domain.Pet.PetStatus status,
        double Weight,
        double Height,
        bool IsCastrated,
        bool IsVaccinated,
        DateTime BirthDate,
        List<RequisiteDto> Requisites
        );


    
}
