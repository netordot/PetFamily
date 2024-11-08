using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Domain;
using PetFamily.Domain.Pet;

namespace PetFamily.API.Contracts
{
    public record AddPetRequest
    // временно speciesBreed не будет указан
    (
        string Name,
        Guid SpeciesId,
        Guid BreedId,
        string Color,
        string Description,
        string HealthCondition,
        // пока что статус будет доменным, когда будет фронт, избавиться от доменна в реквесте
        PetStatus status,
        double Weight,
        double Height,
        bool IsCastrated,
        bool IsVaccinated,
        DateTime BirthDate
        );
}
