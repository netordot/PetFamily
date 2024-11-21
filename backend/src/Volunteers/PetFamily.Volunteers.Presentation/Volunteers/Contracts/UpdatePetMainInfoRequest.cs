using PetFamily.Core.Dtos.PetManagement;

namespace PetFamily.Volunteers.Presentation.Volunteers.Contracts
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
        PetStatusDto status,
        double Weight,
        double Height,
        bool IsCastrated,
        bool IsVaccinated,
        DateTime BirthDate,
        List<RequisiteDto> Requisites
        );



}
