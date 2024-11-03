using PetFamily.Application.Abstractions;
using PetFamily.Domain;
using PetFamily.Domain.Pet.Breed;
using PetFamily.Domain.Pet.Species;

namespace PetFamily.Application.Species
{
    public record CreateSpeciesCommand
    (string SpeciesName, SpeciesId id) : ICommand;
}