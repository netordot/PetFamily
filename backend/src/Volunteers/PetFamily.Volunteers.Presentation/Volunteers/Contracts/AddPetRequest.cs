using PetFamily.Core.Dtos.PetManagement;

namespace PetFamily.Volunteers.Presentation.Volunteers.Contracts
{
    public record AddPetRequest
    (
        string Name,
        Guid SpeciesId,
        Guid BreedId,
        string Color,
        string Description,
        string HealthCondition,
        // пока что статус будет доменным, когда будет фронт, избавиться от доменна в реквесте
        PetStatusDto status,
        double Weight,
        double Height,
        bool IsCastrated,
        bool IsVaccinated,
        DateTime BirthDate,
        IEnumerable<RequisiteDto> Requisites
        );



}
