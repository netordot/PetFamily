using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel.Id;

namespace PetFamily.Species.Application.Commands.CreateSpecies
{
    public record CreateSpeciesCommand
    (string SpeciesName, SpeciesId id) : ICommand;
}