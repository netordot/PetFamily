using PetFamily.Domain;
using PetFamily.Domain.Pet.Breed;

namespace PetFamily.Application.Species
{
    public record CreateSpeciesCommand
    (string SpeciesName, SpeciesId id);
}