using PetFamily.Core.Abstractions;
using PetFamily.Domain;
using PetFamily.Domain.Pet.Breed;
using PetFamily.Domain.Pet.Species;
using PetFamily.SharedKernel.Id;

namespace PetFamily.Application.Species
{
    public record CreateSpeciesCommand
    (string SpeciesName, SpeciesId id) : ICommand;
}