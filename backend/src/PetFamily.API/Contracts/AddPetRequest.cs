using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Domain;

namespace PetFamily.API.Contracts
{
    public record AddPetRequest
    // временно speciesBreed не будет указан
    (
        Guid Id,
        string Name,
        // SpeciesBreedDto SpeciesBreed,
        string Color,
        string Description,
        string HealthCondition,
        // пока что статус будет доменным, когда будет фронт, избавиться от доменна в реквесте
        PetStatus status,
        double Weight,
        double Height,
        bool IsCastrated,
        bool IsVaccinated,
        DateTime BirthDate,
        IFormFileCollection Files
        );
}
